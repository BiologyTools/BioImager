# BioImager

A .NET microscopy imaging application based on Bio. Supports various microscopes by using imported libraries & GUI automation. Supported libraries include Prior® & Zeiss®. Check your manufacturer's SDK to see how to implement "Microscope.cs". Works with or without hardware through microscope simulation. Allows for tiled & depth stack imaging.

## Features

- C# scripting with sample tool-script and other sample scripts in "/Scripts/" folder. [See samples](https://github.com/BioMicroscopy/BioImage-Scripts)

- RGB image viewing mode which automatically combines 3 channels into RGB image & shows ROI from each channel which can be configured in ROI Manager.

- Viewing image stacks with scroll wheel moving Z-plane and mouse side buttons scrolling C-planes.

- Editing & saving ROI's in images to OME format image stacks.

- Copy & Paste to quickly annotate images and name them easily by right click.

- Select multiple points by holding down control key, for delete & move tools. 

- Exporting ROI's from each OME image in a folder of images to CSV.

- Easy freeform annotation with magic select tool which selects based on blob detection.

- Use AForge filters by opening filters tool window and right click to apply. Currently supports only some AForge filters as many of them do not support 16bit and 48bit images. Convert to 8bit image to make use of more filters. Applyed filters can be easily recorded and used in scripts.

## Dependencies
- [BioFormats.Net](https://github.com/GDanovski/BioFormats.Net)
- [IKVM](http://www.ikvm.net/)
- [AForge](http://www.aforgenet.com/)
- [LibTiff.Net](https://bitmiracle.com/libtiff/)
- [Cs-script](https://github.com/oleg-shilo/cs-script/blob/master/LICENSE)

## Licenses
- Bio [GPL3](https://www.gnu.org/licenses/gpl-3.0.en.html)
- AForge [LGPL](http://www.aforgenet.com/framework/license.html)
- BioFormats.Net [GPL3](https://www.gnu.org/licenses/gpl-3.0.en.html)
- [IKVM](https://github.com/gluck/ikvm/blob/master/LICENSE)
- LibTiff.Net [BSD](https://bitmiracle.com/libtiff/)
- Cs-script [MIT](https://github.com/oleg-shilo/cs-script/blob/master/LICENSE)

## Scripting
-  Save scripts into "StartupPath/Scripts" with ".cs" ending.
-  Open script editor and recorder from menu.
-  Double click on script name in Script runner to run script.
-  Scripts saved in Scripts folder will be loaded into script runner.
-  Program installer include sample script "Sample.cs" which gets & sets pixels and saves resulting image.
-  Use Script recorder to record program function calls and script runner to turn recorder text into working scripts. (See sample) [scripts](https://github.com/BioMicroscopy/BioImage-Scripts)

## Sample Tool Script

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


