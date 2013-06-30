using Microsoft.Phone.Shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace TileDemo
{
    public enum TileType { Standard, Flip, Cycle, Iconic };

    class TileHelper
    {
        private static TileHelper _instance;
        public static TileHelper Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new TileHelper();
                }
                return _instance;
            }
        }

        public void CreateTile(TileType tileType, bool supportsWideTile)
        {
            ShellTile shellTile = _FindTile(tileType);
            ShellTileData shellTileData=null;
            switch(tileType)
            {
                case TileType.Standard:

                    shellTileData = _CreateStandardTileData();
                    supportsWideTile = false;
                    break;
                case TileType.Flip:
                    shellTileData = _CreateFlipTileData();
                    break;
                case TileType.Cycle:
                    shellTileData = _CreateCycleTileData();
                    break;
                case TileType.Iconic:
                    shellTileData = _CreateIconicTileData();
                    break;
            }
            if (shellTile != null)
            {
                shellTile.Update(shellTileData);
                //shellTile.Delete();
            }
            else
            {

                ShellTile.Create(new Uri("/Page1.xaml?TileType=" + tileType, UriKind.Relative), shellTileData, supportsWideTile);
            }

        }

        public void UpdateMainTile()
        {
            var mainTile = ShellTile.ActiveTiles.FirstOrDefault();
            if (mainTile != null)
            {
                mainTile.Update(_CreateFlipTileData());
            }
        }

        private ShellTile _FindTile(TileType tileType)
        {
            return ShellTile.ActiveTiles.FirstOrDefault(x => x.NavigationUri.ToString() == "/Page1.xaml?TileType=" + tileType);
        }

        private StandardTileData _CreateStandardTileData()
        {
            var tileData = new StandardTileData
            {
                 BackBackgroundImage=new Uri("/Assets/Tiles/FlipCycleTileMedium.png", UriKind.Relative),
                 BackContent="测试背后的文字",
                 BackgroundImage = new Uri("/Assets/Tiles/FlipCycleTileMedium.png", UriKind.Relative),
                 Count=3,
                 BackTitle = "BackTitle",
                 Title = "Title"
            };

            return tileData;
        }

        private FlipTileData _CreateFlipTileData()
        {

            var tileData = new FlipTileData
            {
                BackBackgroundImage = new Uri("/Assets/Tiles/FlipCycleTileMedium.png", UriKind.Relative),
                BackContent = "测试背后的文字",
                BackgroundImage = new Uri("/Assets/Tiles/FlipCycleTileMedium.png", UriKind.Relative),
                Count = 3,
                BackTitle = "BackTitle",
                Title = "Title",
                SmallBackgroundImage = new Uri("/Assets/Tiles/FlipCycleTileSmall.png", UriKind.Relative),
                WideBackBackgroundImage = new Uri("/Assets/Tiles/FlipCycleTileLarge.png", UriKind.Relative),
                WideBackgroundImage = new Uri("/Assets/Tiles/FlipCycleTileLarge.png", UriKind.Relative),
                WideBackContent = "WideBackContent"
            };

            return tileData;
        }

        private CycleTileData _CreateCycleTileData()
        {
            var tileData = new CycleTileData
            {
                Count = 3,
                Title = "Title",
                SmallBackgroundImage = new Uri("/Assets/Tiles/FlipCycleTileSmall.png", UriKind.Relative),
                CycleImages = new List<Uri>()
                {
                    new Uri("/Assets/Tiles/00.png", UriKind.Relative),
                     new Uri("/Assets/Tiles/01.png", UriKind.Relative),
                      new Uri("/Assets/Tiles/02.png", UriKind.Relative),
                       new Uri("/Assets/Tiles/03.png", UriKind.Relative),
                        new Uri("/Assets/Tiles/04.png", UriKind.Relative),
                       new Uri("/Assets/Tiles/05.png", UriKind.Relative),
                       new Uri("/Assets/Tiles/06.png", UriKind.Relative),
                     new Uri("/Assets/Tiles/07.png", UriKind.Relative),
                      new Uri("/Assets/Tiles/08.png", UriKind.Relative)
                }
            };

            return tileData;

        }

        private IconicTileData _CreateIconicTileData()
        {
            var tileData = new IconicTileData
            {
                Count = 50,
                Title = "Title",
                 BackgroundColor=Colors.Blue,
                IconImage = new Uri("/Assets/Tiles/IconicTileMediumLarge.png", UriKind.Relative),
                SmallIconImage = new Uri("/Assets/Tiles/IconicTileSmall.png", UriKind.Relative),
                WideContent1 = "WideContent1",
                WideContent2 = "WideContent2",
                WideContent3 = "WideContent3"
            };

            return tileData;
        }


    }
}
