﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Collections;
using System.Web.Mvc;
using System.Linq;

namespace Pruefung_Praktisch_Musterloesung.Controllers
{
    public class Lab1Controller : Controller
    {
        /**
         * 
         * ANTWORTEN BITTE HIER
         *
         * Aufgabe 1
         * Man kann im Parameter den Filenamen ändern und auf Bilder zugreifen. Dies wird auch Forceful Browsing genannt.
         * Web Parameter Tampering: Mann kann die Parameter verwenden, um etwas zu ändern. Z.B. könnte man das löschen.
         *
         * Aufgabe 2
         * z.B. http://localhost:4356/Lab1/Detail?file=../../../Windows/System32/cmd.exe?/C+dir+C:\
         *      http://localhost:4356/Lab1/Detail?file=Lion1.jpg&type=lions&action=delete
         *
         * Aufgabe 3
         * Im ersten Beispiel kann man mit ../ aus dem Pfad gehen und die Konsole öffnen und etwas ausfüren
         * Im zweiten Beispiel löscht man das Bild mit dem Parameter action.
         *
         * */


        public ActionResult Index()
        {
            var type = Request.QueryString["type"];

            if (string.IsNullOrEmpty(type))
            {
                type = "lions";                
            }

            var path = "~/Content/images/" + type;

            List<List<string>> fileUriList = new List<List<string>>();

            if (Directory.Exists(Server.MapPath(path)))
            {
                var scheme = Request.Url.Scheme; 
                var host = Request.Url.Host; 
                var port = Request.Url.Port;
                
                string[] fileEntries = Directory.GetFiles(Server.MapPath(path));
                foreach (var filepath in fileEntries)
                {
                    var filename = Path.GetFileName(filepath);
                    var imageuri = scheme + "://" + host + ":" + port + path.Replace("~", "") + "/" + filename;

                    var urilistelement = new List<string>();
                    urilistelement.Add(filename);
                    urilistelement.Add(imageuri);
                    urilistelement.Add(type);

                    fileUriList.Add(urilistelement);
                }
            }
            
            return View(fileUriList);
        }

        public ActionResult Detail()
        {
            var file = Request.QueryString["file"];
            var type = Request.QueryString["type"];

            if (string.IsNullOrEmpty(file))
            {
                file = "Lion1.jpg";
            }
            if (string.IsNullOrEmpty(type))
            {
                file = "lions";
            }

            var relpath = "~/Content/images/" + type + "/" + file;

            List<List<string>> fileUriItem = new List<List<string>>();
            var path = Server.MapPath(relpath);

            if (System.IO.File.Exists(path))
            {
                var scheme = Request.Url.Scheme;
                var host = Request.Url.Host;
                var port = Request.Url.Port;
                var absolutepath = Request.Url.AbsolutePath;

                var filename = Path.GetFileName(file);
                var imageuri = scheme + "://" + host + ":" + port + "/Content/images/" + type + "/" + filename;

                var urilistelement = new List<string>();
                urilistelement.Add(filename);
                urilistelement.Add(imageuri);
                urilistelement.Add(type);

                fileUriItem.Add(urilistelement);
            }
            
            return View(fileUriItem);
        }
    }
}