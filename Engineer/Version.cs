﻿// Kerbal Engineer Redux
// Author:  CYBUTEK
// License: Attribution-NonCommercial-ShareAlike 3.0 Unported

using System;
using System.Collections;
using UnityEngine;

namespace Engineer
{
    class Version
    {
        public const string VERSION = "0.5.4.4";
        public const string PRODUCT_NAME = "engineer_redux";
        private string remoteVersion = null;
        private bool hasCompared = false;
        private bool isNewer = false;

        public bool Same
        {
            get
            {
                if (Remote != Local)
                {
                    return true;
                }
                return false;
            }
        }

        public bool Older
        {
            get
            {
                if (!hasCompared)
                {
                    CompareVersions();
                }
                if (!isNewer && !Same)
                {
                    return true;
                }
                return false;
            }
        }

        public bool Newer
        {
            get
            {
                if (!hasCompared)
                {
                    CompareVersions();
                }
                if (isNewer)
                {
                    return true;
                }
                return false;
            }
        }

        public string Local
        {
            get
            {
                return VERSION;
            }
        }

        public string Remote
        {
            get
            {
                if (remoteVersion == null)
                {
                    remoteVersion = GetRemoteVersion();
                }
                return remoteVersion;
            }
        }

        private void CompareVersions()
        {
            if (Remote == "")
            {
                hasCompared = true;
                return;
            }

            string[] local = Local.Split('.');
            string[] remote = Remote.Split('.');

            if (local.Length > remote.Length)
            {
                Array.Resize<string>(ref remote, local.Length);

                for (int i = 0; i < remote.Length; i++)
                {
                    if (remote[i] == null)
                    {
                        remote[i] = "0";
                    }
                }
            }
            else
            {
                Array.Resize<string>(ref local, remote.Length);

                for (int i = 0; i < local.Length; i++)
                {
                    if (local[i] == null)
                    {
                        local[i] = "0";
                    }
                }
            }

            for (int i = 0; i < local.Length; i++)
            {
                if (Convert.ToInt32(local[i]) < Convert.ToInt32(remote[i]))
                {
                    isNewer = true;
                    break;
                }

                if (Convert.ToInt32(local[i]) > Convert.ToInt32(remote[i]))
                {
                    isNewer = false;
                    break;
                }
            }

            hasCompared = true;
        }

        private string GetRemoteVersion()
        {
            WWW www = new WWW("http://www.cybutek.net/ksp/getversion.php?name=" + PRODUCT_NAME);
            while (!www.isDone) { }
            return www.text;
        }
    }
}