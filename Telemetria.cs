using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text;
using MissionPlanner.Plugin;
using MissionPlanner;
using System.IO;
using MissionPlanner.Utilities;
using System.Threading;

namespace Telemetria
{
    public class TelemPlugin : MissionPlanner.Plugin.Plugin
    {
        public int month;

        Telemetria.TelemetriaUI form;

        public override string Name
        {
            get { return "Telemetria"; }
        }

        public override string Version
        {
            get { return "Version";  }
        }

        public override string Author
        {
            get { return "author"; }
        }

        public override bool Init()
        {
            return true;
        }


        public override bool Loaded()
        {
            ToolStripLabel item = new ToolStripLabel("Telemetria AUVSI");
            item.Click += item_Click;

            Host.FPMenuMap.Items.Add(item);
            
            return true;
        }
        void item_Click(object sender, EventArgs e)
        {
            form = new Telemetria.TelemetriaUI(this);
            form.Show();
         //   loopratehz = 10;
              
        }
        public override bool Loop()
        {        
            return true;
        }
        public override float loopratehz { get; set; }

        public override bool Exit()
        {
            return true;
        }
    }
}


