# BioImager

A .NET microscopy imaging application based on Bio. Supports various microscopes by using imported libraries & GUI automation. Supported libraries include Prior® & Zeiss® & all devices supported by [python-microscope.](https://github.com/python-microscope) If your microscope is not supported check your manufacturer's SDK to implement "Microscope.cs" or use GUI Automation functions. Works with or without hardware through microscope simulation. Allows for tiled & depth stack imaging & supports XInput game controllers to move stage, take images, run ImageJ macros on images or Bio C# scripts. Also check out the wiki for [library usage.](https://github.com/BiologyTools/Bio/wiki/Library-Usage) or check out the [documentation.](https://biologytools.github.io/Documentation/BioImager-3.2.0/html/index.html)

![Nuget](https://img.shields.io/nuget/v/BioImager) ![Nuget](https://img.shields.io/nuget/dt/BioImager) [![DOI](https://zenodo.org/badge/DOI/10.5281/zenodo.8187849.svg)](https://doi.org/10.5281/zenodo.8187849)

## Sample Microscope Script
```
//If the GUI Recording for setting folder is set, we can set the microscope storage folder.
Microscope.SetFolder("F:/Images/5x/");
Microscope.Objectives.SetPosition(0);
Microscope.TakeImage();
Microscope.MoveFieldLeft();
//We can take an image stack based on properties set in the GUI
Microscope.TakeImageStack();
// or by specifying them in script.
Microscope.TakeImageStack(25000,25050,10)
Microscope.MoveFieldDown();
Microscope.TakeTiles(4,4);
Microscope.Objectives.SetPosition(1);
Microscope.SetFolder("F:/Images/10x/");
Microscope.TakeTiles(4,4);
```


## Features

- C# scripting with sample tool-script and other sample scripts in "/Scripts/" folder. [See samples](https://github.com/BioMicroscopy/BioImage-Scripts)

- Supports running ImageJ macro commands on images open in Bio. New Console to run ImageJ macro commands and Bio C# functions.

- Multiple view modes like Emission, and Filtered. ROI's shown for each channel can be configured in ROI Manager.

- Supports drawing shapes & colors onto 16 bit & 48 bit images, unlike System.Drawing.Graphics.

- Convenient viewing of image stacks with scroll wheel moving Z-plane and mouse side buttons scrolling C-planes.

- Editing & saving ROI's in images to OME format image stacks.

- Copy & Paste to quickly annotate images and name them easily by right click.

- Select multiple points by holding down control key, for delete & move tools. 

- Exporting ROI's from each OME image in a folder of images to CSV.

- Easy freeform annotation with magic select tool which selects based on blob detection.

- Use AForge filters by opening filters tool window and right click to apply. Currently supports only some AForge filters as many of them do not support 16bit and 48bit images. Convert to 8bit image to make use of more filters. Applyed filters can be easily recorded and used in scripts. Bio impliments some filters like crop for 16 & 48 bit images.

## Setup
- For detailed Setup instructions see [setup.](https://github.com/BiologyTools/BioImager/wiki/Setup)
- For Zeiss® set the MTB® Api library path to version of MTB® you are using, found in program files.
- For Prior® download Prior® SDK and set path to [Prior® SDK](https://www.prior.com/wp-content/themes/prior-scientific/download.php?file=13594) PriorScientificSDK.dll
- Set Functions to controller buttons by double clicking labels. Functions include all microscope functions & ImageJ & Bio C# scripts.  
- `Star this project on Github to help spread the word about Bio!`

## Dependencies
- [BioFormats.Net](https://github.com/GDanovski/BioFormats.Net)
- [IKVM](http://www.ikvm.net/)
- [AForge](http://www.aforgenet.com/)
- [LibTiff.Net](https://bitmiracle.com/libtiff/)
- [Cs-script](https://github.com/oleg-shilo/cs-script/blob/master/LICENSE)
- [SharpDX](https://github.com/sharpdx/SharpDX)

## Licenses
- Bio [GPL3](https://www.gnu.org/licenses/gpl-3.0.en.html)
- AForge [LGPL](http://www.aforgenet.com/framework/license.html)
- BioFormats.Net [GPL3](https://www.gnu.org/licenses/gpl-3.0.en.html)
- [IKVM](https://github.com/gluck/ikvm/blob/master/LICENSE)
- LibTiff.Net [BSD](https://bitmiracle.com/libtiff/)
- Cs-script [MIT](https://github.com/oleg-shilo/cs-script/blob/master/LICENSE)
- Prior® Scientific EULA v1.0 [Prior® SDK](https://www.prior.com/wp-content/themes/prior-scientific/download.php?file=13594)
- [MTB SDK© 2019 by Carl Zeiss Microscopy](https://www.zeiss.com/microscopy/en/service-support/downloads/micro-toolbox.html)

## Scripting
-  Save scripts into "StartupPath/Scripts" with ".cs" ending.
-  Open script editor and recorder from menu.
-  Double click on script name in Script runner to run script.
-  Scripts saved in Scripts folder will be loaded into script runner.
-  Program installer include sample script "Sample.cs" which gets & sets pixels and saves resulting image.
-  Use Script recorder to record program function calls and script runner to turn recorder text into working scripts. (See sample) [scripts](https://github.com/BioMicroscopy/BioImage-Scripts)

## Sample Tool Script
```
//css_reference Bio.dll;
using System;
using System.Windows.Forms;
using System.Drawing;
using Bio;
using System.Threading;
public class Loader
{

	//Point ROI Tool Example
	public string Load()
	{
		int ind = 1;
		do
		{
			Bio.Scripting.State s = Bio.Scripting.GetState();
			if (s != null)
			{
				if (!s.processed)
				{
					if (s.type == Bio.Scripting.Event.Down && s.buts == MouseButtons.Left)
					{
						ZCT cord = Bio.App.viewer.GetCoordinate();
						Bio.Scripting.LogLine(cord.ToString() + " Coordinate");
						Bio.ROI an = Bio.ROI.CreatePoint(cord, s.p.X, s.p.Y);
						Bio.ImageView.SelectedImage.Annotations.Add(an);
						Bio.Scripting.LogLine(cord.ToString() + " Coordinate");
						an.Text = "Point" + ind;
						ind++;
						Bio.Scripting.LogLine(s.ToString() + " Point");
						//ImageView.viewer.UpdateOverlay();
					}
					else
					if (s.type == Bio.Scripting.Event.Up)
					{
						Bio.Scripting.LogLine(s.ToString());
					}
					else
					if (s.type == Bio.Scripting.Event.Move)
					{
						Bio.Scripting.LogLine(s.ToString());
					}
					s.processed = true;
				}
			}
		} while (true);
		return "OK";
	}
}
```

