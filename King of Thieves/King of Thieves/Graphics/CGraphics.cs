using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace King_of_Thieves.Graphics
{
    static class CGraphics
    {
        private static GraphicsDeviceManager _graphicsInfo;
        public static  SpriteBatch spriteBatch;
        public static RenderTarget2D _rtar2D = null;
        public static GraphicsDevice GPU
        {
            get
            {
                return _graphicsInfo.GraphicsDevice;
            }
        }

        public static void acquireGraphics(ref GraphicsDeviceManager manager)
        {
            _graphicsInfo = manager;
        }

        public static void spawnRenderTarget(ref RenderTarget2D offScreenBuffer)
        {
            _rtar2D = offScreenBuffer;
        }
    }
}
