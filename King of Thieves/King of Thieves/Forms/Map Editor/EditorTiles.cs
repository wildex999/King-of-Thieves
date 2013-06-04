﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace King_of_Thieves.Forms.Map_Editor
{
    public partial class EditorTiles : Form
    {
        Image currentTile = null;
        Texture2D sourceTileSet = null;
        Vector2 cellSize = Vector2.Zero;
        int cellSpacing = 0;
        static Image cursor;
        Vector2 cursorCoords = Vector2.Zero;
        Microsoft.Xna.Framework.Rectangle selectedTile;
        string mainTileSet = "";
        bool changedTileSet = false;
        string previousTileSet = "";
        bool selectorChanged = false;

        public EditorTiles()
        {
            InitializeComponent();
        }

        private void EditorTiles_Load(object sender, EventArgs e)
        {
            //load in tiles
            foreach (KeyValuePair<string, Graphics.CTextureAtlas> kvp in Graphics.CTextures.textures)
            {
                if (kvp.Value.isTileSet)
                    cmbTexture.Items.Add(kvp.Key);
            }

            cmbTexture.SelectedIndex = 0;
            mainTileSet = cmbTexture.Text;
            MemoryStream imageStream = new MemoryStream();
            Texture2D source = CMasterControl.glblContent.Load<Texture2D>("cursorIcon");

            source.SaveAsPng(imageStream, source.Width, source.Height);

            cursor = Image.FromStream(imageStream);

            imageStream.Close();
        }

        public string defaultTileSet
        {
            get
            {
                return mainTileSet;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectorChanged = true;
            MemoryStream imageStream = new MemoryStream();
            sourceTileSet = Graphics.CTextures.textures[cmbTexture.Text].sourceImage;

            //set spacing
            cellSpacing = Graphics.CTextures.textures[cmbTexture.Text].CellSpacing;
            txtSpacing.Text = Graphics.CTextures.textures[cmbTexture.Text].CellSpacing.ToString();
            txtCellSize.Text = Graphics.CTextures.textures[cmbTexture.Text].FrameWidth + "," + Graphics.CTextures.textures[cmbTexture.Text].FrameHeight;
            cellSize = new Vector2(Graphics.CTextures.textures[cmbTexture.Text].FrameWidth, Graphics.CTextures.textures[cmbTexture.Text].FrameHeight);

            if (sourceTileSet.Width > 240)
            {
                hScrollBar1.Enabled = true;
                hScrollBar1.Maximum = sourceTileSet.Width - 240;
                hScrollBar1.Minimum = 0;
            }

            if (sourceTileSet.Height > 244)
            {
                vScrollBar1.Enabled = true;
                vScrollBar1.Maximum = sourceTileSet.Height - 244;
                vScrollBar1.Minimum = 0;
            }

            sourceTileSet.SaveAsPng(imageStream, sourceTileSet.Width, sourceTileSet.Height);

            currentTile = Image.FromStream(imageStream);

            imageStream.Close();
            imageStream = null;

            pictureBox1.Invalidate();
        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.NewValue > e.OldValue)
                cursorCoords.Y -= 1;
            else if (e.NewValue < e.OldValue)
                cursorCoords.Y += 1;

            pictureBox1.Invalidate();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(currentTile, new System.Drawing.Point(-1 * hScrollBar1.Value, -1 * vScrollBar1.Value));
            e.Graphics.DrawImage(cursor, new RectangleF(cursorCoords.X, cursorCoords.Y, cellSize.X, cellSize.Y));
        }

        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.NewValue > e.OldValue)
                cursorCoords.X -= 1;
            else if (e.NewValue < e.OldValue)
                cursorCoords.X += 1;

            pictureBox1.Invalidate();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            System.Drawing.Point clientPoint = pictureBox1.PointToClient(MousePosition);
            cursorCoords = new Vector2((int)clientPoint.X, (int)clientPoint.Y);

            //snap the coordinates to the grid
            float snapX = (float)Math.Floor(cursorCoords.X / cellSize.X), snapY = (float)Math.Floor(cursorCoords.Y / cellSize.Y);

            cursorCoords.X = (snapX * cellSize.X) + (snapX * cellSpacing);
            cursorCoords.Y = (snapY * cellSize.Y) + (snapY * cellSpacing);

            selectedTile = new Microsoft.Xna.Framework.Rectangle((int)cursorCoords.X + hScrollBar1.Value, (int)cursorCoords.Y + vScrollBar1.Value, (int)cellSize.X, (int)cellSize.Y);

            pictureBox1.Invalidate();
        }

        public Microsoft.Xna.Framework.Rectangle tileRect
        {
            get
            {
                return selectedTile;
            }
        }

        public Texture2D sourceSet
        {
            get
            {
                return sourceTileSet;
            }
        }

        private void btnSetMain_Click(object sender, EventArgs e)
        {
            previousTileSet = mainTileSet;
            mainTileSet = cmbTexture.Text;
            changedTileSet = true;
        }

        public bool tileSetChanged
        {
            get
            {
                bool temp = changedTileSet;
                changedTileSet = false;
                return temp;
            }
        }

        public string previousDefaultTileSet
        {
            get
            {
                return previousTileSet;
            }
        }

        public bool selectorChange
        {
            get
            {
                bool temp = selectorChanged;
                selectorChanged = false;
                return temp;
            }

        }
    }
}
