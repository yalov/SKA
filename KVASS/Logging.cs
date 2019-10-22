﻿using System;
using System.Globalization;
using UnityEngine;


namespace KVASSNS
{
    public static class Logging
    {
        private const string PREFIX = "<color=green>[KVASS]</color> ";
        private const bool time = false;

        public static void Log(String msg, params object[] args)
        {
            Debug.Log(PREFIX +
                (time ? DateTime.Now.ToString("HH:mm:ss.f ") : "") +
                String.Format(msg ?? "null", args)
                );
        }

        public static void Log(object arg, params object[] args)
        {
            String log = PREFIX +
                (time ? DateTime.Now.ToString("HH:mm:ss.f ") : "") + arg;

            foreach (var a in args) log += ", " + a;

            Debug.Log(log);
        }
    }
}
