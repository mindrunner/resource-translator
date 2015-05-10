// ***********************************************************************
// Assembly         : ResourceTranslatorLib
// Author           : Lukas Elsner
// Created          : 10-02-2014
//
// Last Modified By : Lukas Elsner
// Last Modified On : 10-09-2014
// ***********************************************************************
// <copyright file="GoogleTranslator.cs" company="mindrunner">
//     Copyright (c) Lukas Elsner. All rights reserved.
// </copyright>
// <summary>Google Translator wrapper to use the service without the non-free API. 
// This code might break in the future without any announcement</summary>
// ***********************************************************************
using System;
using System.Globalization;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;


namespace ResourceTranslatorLib
{
    /// <summary>
    /// Class GoogleTranslator.
    /// </summary>
    public class GoogleTranslator
    {
        /// <summary>
        /// Translates the content from fromLanguage to toLanguage
        /// </summary>
        /// <param name="fromLanguage">From language.</param>
        /// <param name="toLanguage">To language.</param>
        /// <param name="content">The content.</param>
        /// <returns>The translated content, or null if the call failed.</returns>
        public string Translate(CultureInfo fromLanguage, CultureInfo toLanguage, string content)
        {

            {
                string url = string.Format(@"http://translate.google.com/translate_a/t?client=j&text={0}&hl=en&sl={1}&tl={2}",
                                           HttpUtility.UrlEncode(content), fromLanguage, toLanguage);

                /* Retrieve Translation with HTTP GET call */
                string html;
                try
                {
                    var web = new WebClient();

                    /* Must add a known browser user agent or else response encoding doesn't return UTF-8 */
                    web.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/5.0");
                    web.Headers.Add(HttpRequestHeader.AcceptCharset, "UTF-8");

                    /* Make sure we have response encoding to UTF-8 */
                    web.Encoding = Encoding.UTF8;
                    html = web.DownloadString(url);
                }
                catch (Exception)
                {
                    return null;
                }

                /* Extract out trans":"...[Extracted]...","from the JSON string */
                string result = Regex.Match(html, "trans\":(\".*?\"),\"", RegexOptions.IgnoreCase).Groups[1].Value;

                if (string.IsNullOrEmpty(result))
                {
                    return null;
                }

                return result.Substring(1, result.Length - 2);
            }
        }
    }
}