﻿using System;
using System.IO;
using System.Xml;

namespace SpentBook.Importer.Bradesco.Infrastructure.File.Ofx.Reader
{
    public static class OFXHelperMethods
    {
        /// <summary>
        /// Converts string representation of AccountInfo to enum AccountInfo
        /// </summary>
        /// <param name="bankAccountType">representation of AccountInfo</param>
        /// <returns>AccountInfo</returns>
        public static OFXBankAccountType GetBankAccountType(this string bankAccountType)
        {
            return (OFXBankAccountType)Enum.Parse(typeof(OFXBankAccountType), bankAccountType);
        }

        /// <summary>
        /// Flips date from YYYYMMDD to DDMMYYYY         
        /// </summary>
        /// <param name="date">Date in YYYYMMDD format</param>
        /// <returns>Date in format DDMMYYYY</returns>
        public static DateTime ToDate(this string date)
        {
            try
            {
                if (date.Length < 8)
                {
                    return new DateTime();
                }

                var dd = int.Parse(date.Substring(6, 2));
                var mm = int.Parse(date.Substring(4, 2));
                var yyyy = int.Parse(date.Substring(0, 4));

                return new DateTime(yyyy, mm, dd);
            }
            catch
            {
                // throw new OFXParseException("Unable to parse date");
                return DateTime.MinValue;
            }
        }

        /// <summary>
        /// Returns value of specified node
        /// </summary>
        /// <param name="node">Node to look for specified node</param>
        /// <param name="xpath">XPath for node you want</param>
        /// <returns></returns>
        public static string GetValue(this XmlNode node, string xpath)
        {
            // workaround to search values on root node
            var fixedNode = new XmlDocument();
            fixedNode.Load(new StringReader(node.OuterXml));

            var tempNode = fixedNode.SelectSingleNode(xpath);
            return tempNode != null ? tempNode.FirstChild.Value : "";
        }
    }
}