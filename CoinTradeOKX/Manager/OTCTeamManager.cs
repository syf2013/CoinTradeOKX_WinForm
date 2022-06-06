using CoinTradeOKX.Okex.Entity;
using CoinTradeOKX.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoinTradeOKX.Manager
{
    public class OTCTeamManager
    {
        private static OTCTeamManager _instance = null;
        private static object _lock = new object();

        private List<TeamMember> members = new List<TeamMember>();

        private OTCTeamManager()
        {
            this.LoadSettings();
        }

        private string GetSettingPath()
        {
            return Path.Combine(Application.StartupPath, "TeamSettings.json");
        }

        private void LoadSettings()
        {
            lock (this.members)
            {
                string filePath = this.GetSettingPath();
                if (File.Exists(filePath))
                {
                    string str = File.ReadAllText(filePath);
                    List<TeamMember> list = null;
                    try
                    {
                        list = JsonUtil.JsonStringToObject<List<TeamMember>>(str);
                    }
                    catch
                    {
                        return;
                    }

                    this.members.Clear();
                    this.members.AddRange(list);
                }
            }
        }

        private bool SaveSetting()
        {
            string path = this.GetSettingPath();

            lock (this.members)
            {
                
                string str = JsonUtil.ObjectToJsonString(this.members);
                try
                {
                    File.WriteAllText(GetSettingPath(), str);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public List<TeamMember> GetMembers()
        {
            return new List<TeamMember>(this.members);
        }

        public bool IsTeamMember(string name)
        {
            lock(this.members)
            {
                foreach(var m in this.members)
                {
                    if(m.Name == name)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public bool AddMember(string name)
        {
            string err = string.Empty;

            if(string.IsNullOrEmpty(name))
            {
                err = "名称不能为空";
                goto label_err;
            }

            name = name.Trim();

            if (this.IsTeamMember(name))
            {
                err = string.Format("已存在成员{0}",name);
                goto label_err;
            }

            lock (this.members)
            {
                TeamMember m = new TeamMember();
                m.Name = name;
                this.members.Add(m);
            }

            if(!this.SaveSetting())
            {
                err = "存档失败";
                goto label_err;
            }

            return true;

            label_err:
            throw new Exception(err);
        }

        public bool RemoveMember(string name)
        {
            TeamMember member = null;
            lock(this.members)
            {
                foreach(var m in this.members)
                {
                    if(m.Name == name)
                    {
                        member = m;
                        break;
                    }
                }

                if(member != null)
                {
                    this.members.Remove(member);
                }
            }

            if (member != null)
            {
                this.SaveSetting();
                return true;
            }

            return false;
        }

        public static OTCTeamManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            var tm = new OTCTeamManager();
                            _instance = tm;
                        }
                    }
                }

                return _instance;
            }
        }
    }
}
