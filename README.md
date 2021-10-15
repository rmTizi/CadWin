# CadWin

CadWin is a QT style sheet for FreeCAD LinkStage 3 implementing the WinUI 2.7 specification.

Out of the box, this package provides Dark and Light themes with a few accent colors. Most of the work has been focused on the Dark themes, so for now only a dark preset file is provided.

As this is a personal and opionated project, the only tested platform is Windows for now, and only the Part Design, Sketcher, Assembly 3, Spreadsheet, Surface and Mesh workbenches are verified to work without issues.

Things might improve in the future, but if you are a big Part, Draft, TechDraw or Arch user, you might see some rough edges.

If you use Mac OS, make sure that your issue can be reproduced on Windows or Linux, if it's exclusive to your OS I have no way to fix it.

## Quick install

0) Back up your settings, make copies of your user and system files, export your current preset.
1) Download and unzip or clone this repository inside your FreeCAD user folder (the Gui and Settings folder must be at the same level than your link.system.cfg and link.user.cfg)
2) Launch FreeCADLink and make sure you have a file open and peferably the Part Design or Sketcher workbench activated.
3) Go to ```Tools > Preset Configurations > CadWin Dark Preset```.
4) **IMPORTANT** Before doing anything else, grab the workbench tool bar and dock it to the left, then close the extra property view on the right.
5) Restart FreeCAD **DO NOT DO OR CHANGE ANYTHING ELSE BEFORE THIS RESTART OR THINGS WILL BREAK** (Hopefully I'll be able to streamline things in the future)
6) Enjoy the new look, try the different accents in the Preferences dialog.

## Manual install

If you (understandable) do not want your settings to be overwritten by the preset, you can just follow steps 0 and 1 from above, and then pick the style sheet from the Preferences dialog.

You need to set all 3 style sheet options to a CadWin entry for this to work as intended (main, overlay and menu).

You can try to mix and match, but unexpected results are to be expected.

### Considerations

The sylesheet has been designed for vertical tool bars. Horizontal toolbars *do work* but might show quirks and artifacts here and there.

If you are on the main branch (either 0.19 or 0.20), the stylesheet will kinda work, but your are not the target audience and won't be until the main branch adds support for some form of overlays.

## Important notes

The navigation cube might not appear exactly in the spot that is reserved for it depending on your resolution and dpi settings. Just grab and drag it to the free spot on the left of the Selection View

The document tabs are gone. That is by design. I will not change it.

The status bar is off by default, it should remain off unless you have a good reason.

The design is intended for two rows of tool icons on each side.

Tool bar icons with menus behaviour has been changed to only open their menu, they won't accidentaly trigger the last action by error anymore.

## Adding accents and Stitcher tool.

If you want to add more accent colors, just do the following: 

1) Go to ```Gui\Stylesheets\CadWin``` and open either the Dark or Light folder depending on which them you want to use.
2) Open the ```Colors.txt``` file.
3) Add your accent color hex code as a new line and save the file.
4) Go back to ```Gui\Stylesheets\CadWin```
5) Run ```Stitcher.exe```

### Stitcher

The stitcher is a simple self-contained dotnet5 program that stitches the different style files combinations together and output the full stylesheets for use in FreeCAD.

If you do not trust the provided build, the project is provided in the ```Gui\Stylesheets\CadWin\Stitcher``` folder and you can just build and publish it yourself after reviewing its short source code.

It short, it opens the different base .qss files, queery a public API for a human readable color name for the accents, search an replace color and theme tokes with their approriate value and finaly concatenates everything into distinct files.

**IMPORTANT** : The stitcher overwrites without prompt the CadWin .qss files from the ```Stylesheet``` folder, do not do changes in those.

### Manual Stitching

If you cannot or do not want to use the automated tool, you can build a style manualy.

In a new empty .qss file, copy and paste the contents of the folowing files in this order : 

1) ```Layout.qss```, this file defines all the styles that give size position and behaviour to the widgets.
2) ```Icons.qss```, this file contains the url of the replacment icons.
3) ```Theme.qss```, found in Dark and Light folders, this file define the main colors shades, either Dark or Light. It is not advised to make changes in this file.
4) ```Accent.qss```, found in Dark and Light folder, this file contains the accented colors styles.
  
Then you need to search and replace the two following magic string tokens:

1) ```%%THEME%%``` must be changed to ```Cadwin/Dark``` or ```Cadwin/Light``` depending on the theme you want.
2) ```%%R%%, %%G%%, %%B%%``` must be changed to the RGB value of your accent color with the following format ```000, 000, 000```.

If you want to do changes to the style, do your edits in the "source" files (Layout, Icons, Theme, or Accents) and then either run the stitcher or build manually.

Alternatively you can keep a copy not named ```Cadwin <something>.qss``` so that the stitcher won't overwrite it but then you might have to do manual updates too in the future if the Theme changes or improves.

## Final words

While I do hope that you can use and appreciate this stylesheet, please remember that it's a personal side project and I don't intend to pour too many resources into following things up and fixing other people`s issues.

I *do* use this as my daily drive, so it is unlikely to ever fall into a state of disrepair and the quality of life will be rather high if you happen to have a similar workflow to mine, but FreeCAD is a vast tool with many ways, some good and others bad, to do things and I just cannot hope be able to cover everything.

Before opening an issue, please try to reach out on the FreeCAD discord in the #ui-ux-themes channel, It will be faster and easier for me to tell you if I can even fix it or not.

## License

None, do as you please, it's just a stylesheet and a few lines of C#.

Icons are from the Segoe Fuent Iconts font, just saved as .svg because QT doesn't support text icons, if you are on Windows you already have it on your system.
