using System;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

using static System.IO.File;
using static System.IO.Path;

namespace Stitcher
{
    class Program
    {
        private static readonly HttpClient theColorApi = new() { BaseAddress = new Uri("http://www.thecolorapi.com") };
        public record ColorName(string Value);
        public record ColorInfo(ColorName Name);

        public static async Task Main()
        {
            Console.WriteLine($"Launched from {Environment.CurrentDirectory}");
#if DEBUG
            string basePath = Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"FreeCAD\Gui\Stylesheets\CadWin");
#else
            string basePath = Environment.CurrentDirectory;
#endif
            string stylesheetsPath = System.IO.Directory.GetParent(basePath).ToString();

            string themeToken = "%%THEME%%";
            string colorToken = "%%R%%, %%G%%, %%B%%";

            string themeParam(string theme) => $"CadWin/{theme}";
            string colorParam(Color color) => $"{color.R}, {color.G}, {color.B}";

            string[] themes() => new[] { "Dark", "Light" };
            string[] colors(string theme) => ReadAllLines(Combine(basePath, theme, "Colors.txt"));

            string layoutStyle() => ReadAllText(Combine(basePath, "Layout.qss"));
            string iconsStyle(string theme) => ReadAllText(Combine(basePath, "Icons.qss")).Replace(themeToken, themeParam(theme));
            string themeStyle(string theme) => ReadAllText(Combine(basePath, theme, "Theme.qss"));
            string accentStyle(string theme, Color color) => ReadAllText(Combine(basePath, theme, "Accent.qss")).Replace(colorToken, colorParam(color));

            string stylePath(string theme, ColorInfo accent) => Combine(stylesheetsPath, $"CadWin {theme} {accent.Name.Value}.qss");

            var styles = themes()
                .Select(theme => (theme, contents: String.Concat(layoutStyle(), iconsStyle(theme), themeStyle(theme))))
                .SelectMany(style => colors(style.theme)
                    .Select(async hex => (style.theme,
                        accent: await theColorApi.GetFromJsonAsync<ColorInfo>($"id?hex={hex[1..]}"),
                        contents: String.Concat(style.contents, accentStyle(style.theme, ColorTranslator.FromHtml(hex))))));

            var files = (await Task.WhenAll(styles))
                .Select(style => (filename: stylePath(style.theme, style.accent), style.contents));

            foreach (var (filename, contents) in files)
            {
                Console.WriteLine($"Writing {filename}");
                WriteAllText(filename, contents);
            }
            Console.WriteLine("Done");
        }
    }
}
