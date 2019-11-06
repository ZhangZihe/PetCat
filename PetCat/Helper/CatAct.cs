using PetCat.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PetCat
{
    public enum ActType
    {
        Tail = 0,
        Hit = 1,
        Eat = 2,
        Walk = 3,
        WalkR = 4,
        Hungry = 5,
    }

    class CatAct
    {
        public ActType LastAct { get; set; }

        public List<ActBag> Animations { get; set; }

        private int curFrame; //当前帧 
        private int rateX = -2; //水平速度
        private int rateY = 0;  //垂直速度
        private ActType curAct;
        private MainFrm cat;
        public Point TipPoint;
        private Random random = new Random();

        public CatAct(MainFrm cat)
        {
            this.cat = cat;
            this.cat.TimerMain.Tick += TimerMain_Tick;
            this.TipPoint = new Point(cat.Width / 2 + 10, 20);
        }

        public ActType CurAct
        {
            get { return curAct; }
            set
            {
                cat.Enabled = false;
                curAct = value;
                curFrame = 0;
                cat.ToolTip.SetToolTip(cat, "");
                
                if (curAct == ActType.Walk)
                    rateX = -Math.Abs(rateX);
                else if (curAct == ActType.WalkR)
                    rateX = Math.Abs(rateX);
                else if (curAct == ActType.Eat)
                    cat.ToolTip.Show(Resources.Eat, cat, TipPoint, 1000);
                else if (curAct == ActType.Hungry)
                    cat.ToolTip.Show(Resources.Hungry, cat, TipPoint, 1000);
                else if (curAct == ActType.Hit)
                    cat.ToolTip.Show(Resources.Hit, cat, TipPoint, 1000);
                cat.Enabled = true;
            }
        }

        public void LoadResources(string resourcesPath)
        {
            var rootUrl = Path.Combine(resourcesPath, "Act");
            var actDirs = new DirectoryInfo(rootUrl);
            Animations = new List<ActBag>();

            foreach (var dirInfo in actDirs.GetDirectories())
            {
                if (!Enum.TryParse(dirInfo.Name, true, out ActType actType))
                    continue;

                var acts = dirInfo.GetFiles().OrderBy(x => int.Parse(x.Name.Split('.')[0])).Select(x => (Bitmap)Image.FromStream(x.OpenRead())).ToList();
                Animations.Add(new ActBag(actType, acts));
            }

            var walkR = Animations.Where(x => x.ActType == ActType.Walk).First().Animations.Select(x => ((Bitmap)x.Clone())).ToList();
            walkR.ForEach(x => x.RotateFlip(RotateFlipType.RotateNoneFlipX));
            Animations.Add(new ActBag(ActType.WalkR, walkR));
        }

        private void TimerMain_Tick(object sender, EventArgs e)
        {
            var curActBag = Animations.FirstOrDefault(x => x.ActType == CurAct);
            if (curActBag == null)
                return;

            if (CurAct == ActType.Walk || CurAct == ActType.WalkR)
                FreeMove();

            var curAnimation = curActBag.Animations.ElementAt(curFrame++);
            cat.SetBitmap(curAnimation);

            if (curFrame >= curActBag.FrameCount)
            {
                curFrame = 0;
                if (CurAct != ActType.Walk && CurAct != ActType.WalkR)
                    curAct = ActType.Tail;
            }

            if (cat.Enabled)
                RandomAct();
        }

        private void RandomAct()
        {
            int randomNum = random.Next(1000000);
            if (randomNum >= 0 && randomNum < 5000 && CurAct == ActType.Walk)
                CurAct = ActType.Tail;
            if (randomNum >= 5000 && randomNum < 7000 && CurAct == ActType.Tail)
                CurAct = ActType.Walk;
            if (randomNum >= 7000 && randomNum < 9000 && CurAct == ActType.Walk)
                CurAct = ActType.WalkR;
            if (randomNum >= 9000 && randomNum < 9500 && (CurAct == ActType.Walk || CurAct == ActType.Tail))
                CurAct = ActType.Hit;
            if (randomNum >= 9500 && randomNum < 9800 && (CurAct == ActType.Walk || CurAct == ActType.Tail))
                CurAct = ActType.Eat;
            if (randomNum >= 9800 && randomNum < 10000 && (CurAct == ActType.Walk || CurAct == ActType.Tail))
                CurAct = ActType.Hungry;
        }

        private void FreeMove()
        {
            cat.Top += rateY;
            cat.Left += rateX;

            Rectangle rect = SystemInformation.VirtualScreen;
            int screenWidth = rect.Width;
            int screenHeight = rect.Height;

            if (cat.Left < rect.X && CurAct == ActType.Walk) CurAct = ActType.WalkR;
            if (cat.Left + cat.Width > screenWidth + rect.X && CurAct == ActType.WalkR) CurAct = ActType.Walk;
            if (cat.Top < 0 && rateY < 0) rateY = -rateY;
            if (cat.Top + cat.Height > screenHeight && rateY > 0) rateY = -rateY;
        }



        public class ActBag
        {
            public ActBag(ActType actType, List<Bitmap> animations)
            {
                ActType = actType;
                Animations = animations;
                FrameCount = animations.Count();
            }
            
            public ActType ActType { get; set; }

            public List<Bitmap> Animations { get; set; }

            public int FrameCount { get; private set; }
        }
    }
}
