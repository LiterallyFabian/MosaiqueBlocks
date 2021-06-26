# MosaiqueBlocks

MosaiqueBlocks automatically replaces pixels in Minecraft textures with a block from the game.

![Banner](https://i.imgur.com/ZlhPbGa.jpeg)

The concept have been around for years and is not original, this is just something random I put together in two hours. Due to copyright laws I'm not allowed to distribute the texture pack here, but feel free to generate it yourself with the program or download it from one of many [texture pack websites](https://www.curseforge.com/minecraft/texture-packs/pixelblocks/).



## What textures will the program replace?

Everything that have a texture will be replaced, with a few exceptions (such as fonts, colormaps and liquid overlays). All textures that are 16x16 and does not contain a transparent pixel will be used for replacements.

![Examples](https://i.imgur.com/gah5uDB.jpeg)



## Usage

#### Get the source code

```
git clone https://github.com/LiterallyFabian/MosaiqueBlocks
cd MosaiqueBlocks
```

#### Extract the textures from Minecraft 

Open `%appdata%\.minecraft\versions\1.17\1.17.jar` and copy the `assets` directory to the same directory where you have the MosaiqueBlocks executable. **Do not** run the Jar file, open it with a program like [WinRAR](https://www.win-rar.com/).

#### Use the texture pack

After you have run the program and generated all textures, create a new directory in `%appdata%\.minecraft\resourcepacks`, put the `output/assets` directory and [this](https://gist.github.com/LiterallyFabian/bdcad543a500431f16d486525d2cc636) file in there and run Minecraft.

