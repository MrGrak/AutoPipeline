using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;
using System.Text;

namespace Game1
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public enum ProjectType { DirectX, OpenGL }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);


            #region Build a mgcb file based on folder contents

            //first, where is the content folder located? read/write to this folder
            string pathToContent = @"C:\Users\Gx000000\Desktop\REPOs\AutoPipeline\Game1\Game1\Content\";

            //setup a string builder that we can write lots of strings without creating garbage
            StringBuilder SB = new StringBuilder(4096);

            //determine what type of MGCB file we should write
            ProjectType PT = ProjectType.DirectX; //Windows
            bool HiDef = true;
            bool Compress = true;

            //write global properties
            SB.AppendLine(@" ");
            SB.AppendLine(@"#----------------------------- Global Properties ----------------------------#");
            SB.AppendLine(@" ");
            SB.AppendLine(@"/outputDir:bin/$(Platform)");
            SB.AppendLine(@"/intermediateDir:obj/$(Platform)");

            //select directx/windows or assume opengl by default
            if (PT == ProjectType.DirectX) { SB.AppendLine(@"/platform:Windows"); }
            else { SB.AppendLine(@"/platform:DesktopGL"); }

            SB.AppendLine(@"/config:");

            //write hi def property or not
            if (HiDef) { SB.AppendLine(@"/profile:HiDef"); }
            else { SB.AppendLine(@"/profile:Reach"); }

            //write compress property or not
            if (Compress) { SB.AppendLine(@"/compress:True"); }
            else { SB.AppendLine(@"/compress:False"); }

            //write references section
            SB.AppendLine(@" ");
            SB.AppendLine(@"#-------------------------------- References --------------------------------#");
            SB.AppendLine(@" ");

            //write content section
            SB.AppendLine(@" ");
            SB.AppendLine(@"#---------------------------------- Content ---------------------------------#");

            //use directory info to get file.name (so we dont have to trim/split full path filenames)
            DirectoryInfo d = new DirectoryInfo(pathToContent);

            //collect and write all png files
            FileInfo[] PngFiles = d.GetFiles("*.png");
            foreach(FileInfo file in PngFiles)
            {
                SB.AppendLine(@" ");
                SB.AppendLine(@"#begin " + file.Name);
                SB.AppendLine(@"/importer:TextureImporter");
                SB.AppendLine(@"/processor:TextureProcessor");
                SB.AppendLine(@"/processorParam:ColorKeyColor=255,0,255,255");
                SB.AppendLine(@"/processorParam:ColorKeyEnabled=True");
                SB.AppendLine(@"/processorParam:GenerateMipmaps=False");
                SB.AppendLine(@"/processorParam:PremultiplyAlpha=True");
                SB.AppendLine(@"/processorParam:ResizeToPowerOfTwo=False");
                SB.AppendLine(@"/processorParam:MakeSquare=False");
                SB.AppendLine(@"/processorParam:TextureFormat=Color");
                SB.AppendLine(@"/build:" + file.Name);
            }

            //collect and write all wav files
            FileInfo[] WavFiles = d.GetFiles("*.wav");
            foreach (FileInfo file in WavFiles)
            {
                SB.AppendLine(@" ");
                SB.AppendLine(@"#begin " + file.Name);
                SB.AppendLine(@"/importer:WavImporter");
                SB.AppendLine(@"/processor:SoundEffectProcessor");
                SB.AppendLine(@"/processorParam:Quality=Best");
                SB.AppendLine(@"/build:" + file.Name);
            }

            //collect and write all bmp files
            FileInfo[] BmpFiles = d.GetFiles("*.bmp");
            foreach (FileInfo file in BmpFiles)
            {
                SB.AppendLine(@" ");
                SB.AppendLine(@"#begin " + file.Name);
                SB.AppendLine(@"/importer:TextureImporter");
                SB.AppendLine(@"/processor:FontTextureProcessor");
                SB.AppendLine(@"/processorParam:FirstCharacter=");
                SB.AppendLine(@"/processorParam:PremultiplyAlpha=True");
                SB.AppendLine(@"/processorParam:TextureFormat=Color");
                SB.AppendLine(@"/build:" + file.Name);
            }

            //collect and write all fx files
            FileInfo[] FxFiles = d.GetFiles("*.fx");
            foreach (FileInfo file in FxFiles)
            {
                SB.AppendLine(@" ");
                SB.AppendLine(@"#begin " + file.Name);
                SB.AppendLine(@"/importer:EffectImporter");
                SB.AppendLine(@"/processor:EffectProcessor");
                SB.AppendLine(@"/processorParam:DebugMode=Auto");
                SB.AppendLine(@"/build:" + file.Name);
            }

            //finally, write all text to mgcb file
            string MGCB_filepath = pathToContent + @"Content_WIP.mgcb";
            File.WriteAllText(MGCB_filepath, SB.ToString());

            #endregion


        }

        protected override void UnloadContent() { }
        
        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            base.Draw(gameTime);
        }
    }
}