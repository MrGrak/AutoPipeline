# AutoPipeline
automates the process of adding assets to mgcb files, via c# script.

once upon a time a fox made NoPipeline, and while it was useful, it wasn't well supported or flexible to users needs. So, as an example, AutoPipeline was created.

AutoPipeline isn't a library! it's an example of how to write .mgcb files based on a folder of assets. so instead of adding pngs, wavs, bmps, and fx files by hand (using the pipeline tool), you can just run a script and have that mgcb file generated for you. then you open the mgcb file using the pipeline tool, build it, and save it. that's it.

you will find the AutoPipeline's code in the above game1.cs file. take this code and modify it to suit your needs. 

the expected workflow is:
copy paste all your assets (pngs, wavs, fx, bmps, etc) into the content folder.
run AutoPipeline script to generate a mgcb file.
open generated mgcb file and build it, then save it.

Mit License, 2022.
