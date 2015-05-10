// ***********************************************************************
// Assembly         : ResourceTranslatorLib
// Author           : Lukas Elsner
// Created          : 10-07-2014
//
// Last Modified By : Lukas Elsner
// Last Modified On : 10-09-2014
// ***********************************************************************
// <copyright file="TmxExporter.cs" company="mindrunner">
//     Copyright (c) Lukas Elsner. All rights reserved.
// </copyright>
// <summary>Exports the translated items to a project specific TMX file</summary>
// ***********************************************************************
using System;
using System.IO;
using System.Xml;


namespace ResourceTranslatorLib
{
    /// <summary>
    /// Class TmxExporter.
    /// </summary>
    public class TmxExporter
    {
        /// <summary>
        /// The Sessionmodel
        /// </summary>
        private readonly TranslatorSessionModel _model;

        /// <summary>
        /// Initializes a new instance of the <see cref="TmxExporter"/> class.
        /// </summary>
        /// <param name="translationItems">The translation items.</param>
        /// <param name="model">The model.</param>
        public TmxExporter(TranslatorSessionModel model)
        {
            _model = model;
        }


        /// <summary>
        /// Exports the specified file name.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public void Export(String fileName)
        {
            var fileStream = new FileStream(fileName, FileMode.Create);
            var sw = new StreamWriter(fileStream);
            var writer = new XmlTextWriter(sw) { Formatting = Formatting.Indented, Indentation = 2 };

            writer.WriteStartDocument();
            writer.WriteStartElement("tmx");
            writer.WriteAttributeString("version", "1.4");

            writer.WriteStartElement("header");
            writer.WriteAttributeString("creationtool", "Resource Translator");
            writer.WriteAttributeString("creationtoolversion", "1.0");
            writer.WriteAttributeString("datatype", "PlainText");
            writer.WriteAttributeString("segtype", "sentence");
            writer.WriteAttributeString("adminlang", _model.SourceLanguageTmx.ToString());
            writer.WriteAttributeString("srclang", _model.SourceLanguageTmx.ToString());
            writer.WriteAttributeString("o-tmf", "TMX");
            writer.WriteEndElement(); //header
            writer.WriteStartElement("body");
            foreach (var translationItem in _model.TranslationItems)
            {
                writer.WriteStartElement("tu");

                writer.WriteStartElement("tuv");
                writer.WriteAttributeString("xml", "lang", null, _model.SourceLanguageTmx.ToString());
                writer.WriteElementString("seg", translationItem.Source);
                writer.WriteEndElement(); // tuv

                writer.WriteStartElement("tuv");
                writer.WriteAttributeString("xml", "lang", null, _model.DestinationLanguage.ToString());
                writer.WriteElementString("seg", translationItem.Target);
                writer.WriteEndElement(); // tuv

                writer.WriteEndElement(); // tu
            }
            writer.WriteEndElement(); // body
            writer.WriteEndElement(); // tmx
            writer.WriteEndDocument();
            writer.Close();
        }
    }
}
