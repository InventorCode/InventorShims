using Inventor;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InventorShims
{
    /// <summary>
    /// A set of methods to interact with Document Parameters in a more intuitive way.  They will tend to fail more
    /// gracefully than the built-in parameter manipulation methods.  For example, if you attempt to set the value
    /// of a parameter that does not exist in the document, that parameter is created instead of throwing an exception
    /// and crashing your code if you don't handle it.
    /// </summary>
    public static class ParameterShim
    {
        /// <summary>
        /// Sets the value of a numeric parameter, or creates one if one does not exist.
        /// <code></code>VB sample:<code>SetParameterValue(oDoc, "ParameterName", 42, "in"))</code>
        /// </summary>
        /// <param name="document">Inventor.Document</param>
        /// <param name="parameterName"></param>
        /// <param name="parameterValue"></param>
        /// <param name="units">Units as a string, e.g. in, mm, cm, etc</param>
        /// <param name="clobberFlag">If set to false, will not overwrite an existing parameter.</param>
        public static void SetParameterValue(this Document document, string parameterName, string parameterValue, string units, bool clobberFlag = true)
        {
            Parameter parameter = document.GetParameter(parameterName);

            if (!ParameterExists(parameter))
            {
                try
                {
                    Parameters parameters = document.GetParameters();
                    parameters.UserParameters.AddByExpression(parameterName, parameterValue, units);
                }
                catch { }
                return;
            }

            if (clobberFlag && ParameterIsWritable(parameter))
            {
                try
                {
                    parameter.Expression = parameterValue + " " + units;
                }
                catch { }
            }
        }

        /// <summary>
        /// Sets the value of a numeric parameter, or creates one if one does not exist.
        /// <code></code>VB sample:<code>SetParameterValue(oAssemblyDoc, "ParameterName", 42, "in"))</code>
        /// </summary>
        /// <param name="document"></param>
        /// <param name="parameterName"></param>
        /// <param name="parameterValue"></param>
        /// <param name="units"></param>
        /// <param name="clobberFlag">If set to false, will not overwrite an existing parameter.</param>
        public static void SetParameterValue(this AssemblyDocument document, string parameterName, string parameterValue, string units, bool clobberFlag = true)
            => SetParameterValue((Document)document, parameterName, parameterValue, units, clobberFlag);

        /// <summary>
        /// Sets the value of a numeric parameter, or creates one if one does not exist.
        /// <code></code>VB sample:<code>SetParameterValue(oPartDoc, "ParameterName", 42, "in"))</code>
        /// </summary>
        /// <param name="document"></param>
        /// <param name="parameterName"></param>
        /// <param name="parameterValue"></param>
        /// <param name="units"></param>
        /// <param name="clobberFlag">If set to false, will not overwrite an existing parameter.</param>
        public static void SetParameterValue(this PartDocument document, string parameterName, string parameterValue, string units, bool clobberFlag = true)
            => SetParameterValue((Document)document, parameterName, parameterValue, units, clobberFlag);

        /// <summary>
        /// Sets the value of a numeric parameter, or creates one if one does not exist.
        /// <code></code>VB sample:<code>SetParameterValue(oDrawingDoc, "ParameterName", 42, "in"))</code>
        /// </summary>
        /// <param name="document"></param>
        /// <param name="parameterName"></param>
        /// <param name="parameterValue"></param>
        /// <param name="units"></param>
        /// <param name="clobberFlag">If set to false, will not overwrite an existing parameter.</param>
        public static void SetParameterValue(this DrawingDocument document, string parameterName, string parameterValue, string units, bool clobberFlag = true)
    => SetParameterValue((Document)document, parameterName, parameterValue, units, clobberFlag);

        /// <summary>
        /// Sets the value of a text parameter, or creates one if one does not exist.
        /// <code></code>VB sample:<code>SetParameterValue(oDoc, "ParameterName", "This is a text value!"))</code>
        /// </summary>
        /// <param name="document">Inventor.Document</param>
        /// <param name="parameterName"></param>
        /// <param name="parameterValue"></param>
        /// <param name="clobberFlag">If set to false, will not overwrite an existing parameter.</param>
        public static void SetParameterValue(this Document document, string parameterName, string parameterValue, bool clobberFlag = true)
        {
            Parameter parameter = document.GetParameter(parameterName);

            if (!ParameterExists(parameter))
            {
                try
                {
                    Parameters parameters = document.GetParameters();
                    parameters.UserParameters.AddByValue(parameterName, parameterValue, UnitsTypeEnum.kTextUnits);
                }
                catch { }
                return;
            }

            var unit = parameter.get_Units();

            if (clobberFlag && ParameterIsWritable(parameter) && unit.Equals("Text", StringComparison.OrdinalIgnoreCase))
            {
                try
                {
                    parameter.Value = parameterValue;
                }
                catch { }
            }
        }

        /// <summary>
        /// Sets the value of a text parameter, or creates one if one does not exist.
        /// <code></code>VB sample:<code>SetParameterValue(oAssemblyDoc, "ParameterName", "This is a text value!"))</code>
        /// </summary>
        /// <param name="document"></param>
        /// <param name="parameterName"></param>
        /// <param name="parameterValue"></param>
        /// <param name="clobberFlag">If set to false, will not overwrite an existing parameter.</param>
        public static void SetParameterValue(this AssemblyDocument document, string parameterName, string parameterValue, bool clobberFlag = true)
            => SetParameterValue((Document)document, parameterName, parameterValue, clobberFlag);

        /// <summary>
        /// Sets the value of a text parameter, or creates one if one does not exist.
        /// <code></code>VB sample:<code>SetParameterValue(oPartDoc, "ParameterName", "This is a text value!"))</code>
        /// </summary>
        /// <param name="document"></param>
        /// <param name="parameterName"></param>
        /// <param name="parameterValue"></param>
        /// <param name="clobberFlag">If set to false, will not overwrite an existing parameter.</param>
        public static void SetParameterValue(this PartDocument document, string parameterName, string parameterValue, bool clobberFlag = true)
    => SetParameterValue((Document)document, parameterName, parameterValue, clobberFlag);

        /// <summary>
        /// Sets the value of a text parameter, or creates one if one does not exist.
        /// <code></code>VB sample:<code>SetParameterValue(oDrawingDoc, "ParameterName", "This is a text value!"))</code>
        /// </summary>
        /// <param name="document"></param>
        /// <param name="parameterName"></param>
        /// <param name="parameterValue"></param>
        /// <param name="clobberFlag"></param>
        public static void SetParameterValue(this DrawingDocument document, string parameterName, string parameterValue, bool clobberFlag = true)
    => SetParameterValue((Document)document, parameterName, parameterValue, clobberFlag);

        /// <summary>
        /// Sets the value of a boolean parameter, or creates one if one does not exist.
        /// <code></code>VB sample:<code>SetParameterValue(oDoc, "ParameterName", true))</code>
        /// </summary>
        /// <param name="document">Inventor.Document</param>
        /// <param name="parameterName"></param>
        /// <param name="parameterValue"></param>
        /// <param name="clobberFlag">If set to false, will not overwrite an existing parameter.</param>
        public static void SetParameterValue(this Document document, string parameterName, bool parameterValue, bool clobberFlag = true)
        {
            Parameter parameter = document.GetParameter(parameterName);

            if (!ParameterExists(parameter))
            {
                try
                {
                    Parameters parameters = document.GetParameters();
                    parameters.UserParameters.AddByValue(parameterName, parameterValue, UnitsTypeEnum.kBooleanUnits);
                }
                catch { }
                return;
            }

            var unit = parameter.get_Units();

            if (clobberFlag && ParameterIsWritable(parameter) && unit.Equals("Boolean", StringComparison.OrdinalIgnoreCase))
            {
                try
                {
                    parameter.Value = parameterValue;
                }
                catch { }
            }
        }

        /// <summary>
        /// Sets the value of a boolean parameter, or creates one if one does not exist.
        /// <code></code>VB sample:<code>SetParameterValue(oAssemblyDoc, "ParameterName", true))</code>
        /// </summary>
        /// <param name="document"></param>
        /// <param name="parameterName"></param>
        /// <param name="parameterValue"></param>
        /// <param name="clobberFlag">If set to false, will not overwrite an existing parameter.</param>
        public static void SetParameterValue(this AssemblyDocument document, string parameterName, bool parameterValue, bool clobberFlag = true)
            => SetParameterValue((Document)document, parameterName, parameterValue, clobberFlag);

        /// <summary>
        /// Sets the value of a boolean parameter, or creates one if one does not exist.
        /// <code></code>VB sample:<code>SetParameterValue(oPartDoc, "ParameterName", true))</code>
        /// </summary>
        /// <param name="document"></param>
        /// <param name="parameterName"></param>
        /// <param name="parameterValue"></param>
        /// <param name="clobberFlag">If set to false, will not overwrite an existing parameter.</param>
        public static void SetParameterValue(this PartDocument document, string parameterName, bool parameterValue, bool clobberFlag = true)
            => SetParameterValue((Document)document, parameterName, parameterValue, clobberFlag);

        /// <summary>
        /// Sets the value of a boolean parameter, or creates one if one does not exist.
        /// <code></code>VB sample:<code>SetParameterValue(oDrawingDoc, "ParameterName", true))</code>
        /// </summary>
        /// <param name="document"></param>
        /// <param name="parameterName"></param>
        /// <param name="parameterValue"></param>
        /// <param name="clobberFlag">If set to false, will not overwrite an existing parameter.</param>
        public static void SetParameterValue(this DrawingDocument document, string parameterName, bool parameterValue, bool clobberFlag = true)
            => SetParameterValue((Document)document, parameterName, parameterValue, clobberFlag);

        /// <summary>
        /// Gets the value of a numeric parameter.
        /// <code></code>VB sample:<code>Msgbox(GetParameterValue(oDoc, "ParameterName"))</code>
        /// </summary>
        /// <param name="document">Inventor.Document</param>
        /// <param name="parameterName">Name of the parameter as a string.</param>
        /// <returns></returns>
        public static string GetParameterValue(this Document document, string parameterName)
        {
            Parameter parameter = document.GetParameter(parameterName);

            UnitsOfMeasure uom = document.UnitsOfMeasure;

            if (!ParameterExists(parameter))
            {
                return String.Empty;
            }

            var value = parameter.Value;
            var unit = parameter.get_Units();

            UnitsTypeEnum unitEnum;
            unitEnum = uom.GetTypeFromString(unit);

            switch (unitEnum)
            {
                case UnitsTypeEnum.kBooleanUnits:
                    return (string)parameter.Value;

                case UnitsTypeEnum.kTextUnits:
                    return (string)parameter.Value;

                default:
                    return uom.GetStringFromValue((double)value, unitEnum);
            }
        }

        /// <summary>
        /// Gets the value of a numeric parameter.
        /// <code></code>VB sample:<code>Msgbox(GetParameterValue(oAssemblyDoc, "ParameterName"))</code>
        /// </summary>
        /// <param name="document"></param>
        /// <param name="parameterName"></param>
        /// <returns></returns>
        public static string GetParameterValue(this AssemblyDocument document, string parameterName)
            => GetParameterValue((Document)document, parameterName);

        /// <summary>
        /// Gets the value of a numeric parameter.
        /// <code></code>VB sample:<code>Msgbox(GetParameterValue(oPartDoc, "ParameterName"))</code>
        /// </summary>
        /// <param name="document"></param>
        /// <param name="parameterName"></param>
        /// <returns></returns>
        public static string GetParameterValue(this PartDocument document, string parameterName)
            => GetParameterValue((Document)document, parameterName);

        /// <summary>
        /// Gets the value of a numeric parameter.
        /// <code></code>VB sample:<code>Msgbox(GetParameterValue(oDrawingDoc, "ParameterName"))</code>
        /// </summary>
        /// <param name="document"></param>
        /// <param name="parameterName"></param>
        /// <returns></returns>
        public static string GetParameterValue(this DrawingDocument document, string parameterName)
            => GetParameterValue((Document)document, parameterName);

        /// <summary>
        /// Removes a parameter from a Document object if it exists.
        ///   <code></code>VB sample:<code>RemoveParameter(oDoc, "ParameterName")</code>
        /// </summary>
        /// <param name="document">Inventor.Document</param>
        /// <param name="parameterName"></param>
        public static void RemoveParameter(this Document document, string parameterName)
        {
            //Parameters parameters = GetParameters(document);
            //_ = parameters ?? throw new ArgumentException("This document " + document.FullDocumentName + " does not support parameters.");
            Parameter parameter = document.GetParameter(parameterName);

            if (!ParameterExists(parameter))
            {
                return;
            }

            if (!ParameterIsWritable(parameter))
            {
                return;
            }

            if (parameter.InUse)
            {
                return;
            }

            parameter.Delete();
        }

        /// <summary>
        /// Removes a parameter from a Document object if it exists.
        ///   <code></code>VB sample:<code>RemoveParameter(oAssemblyDoc, "ParameterName")</code>
        /// </summary>
        /// <param name="document"></param>
        /// <param name="parameterName"></param>
        public static void RemoveParameter(this AssemblyDocument document, string parameterName)
            => RemoveParameter((Document)document, parameterName);

        /// <summary>
        /// Removes a parameter from a Document object if it exists.
        ///   <code></code>VB sample:<code>RemoveParameter(oPartDoc, "ParameterName")</code>
        /// </summary>
        /// <param name="document"></param>
        /// <param name="parameterName"></param>
        public static void RemoveParameter(this PartDocument document, string parameterName)
            => RemoveParameter((Document)document, parameterName);

        /// <summary>
        /// Removes a parameter from a Document object if it exists.
        ///   <code></code>VB sample:<code>RemoveParameter(oDrawingDoc, "ParameterName")</code>
        /// </summary>
        /// <param name="document"></param>
        /// <param name="parameterName"></param>
        public static void RemoveParameter(this DrawingDocument document, string parameterName)
            => RemoveParameter((Document)document, parameterName);

        /// <summary>
        /// Returns a parameter object from a document object specified by name.
        ///  <code></code>VB sample:<code>Dim oParameter As Parameter = GetParameter(oDoc, "ParameterName")</code>
        /// </summary>
        /// <param name="document">Inventor.Document</param>
        /// <param name="parameterName"></param>
        /// <returns>Parameter</returns>
        public static Parameter GetParameter(this Document document, string parameterName)
        {
            Parameters parameters = GetParameters(document);
            _ = parameters ?? throw new ArgumentException("This document " + document.FullDocumentName + " does not support parameters.");

            foreach (Parameter parameter in parameters)
            {
                if (parameter.Name == parameterName) { return parameter; }
            }

            return null;
        }

        /// <summary>
        /// Returns a parameter object from an AssemblyDocument object specified by name.
        ///  <code></code>VB sample:<code>Dim oParameter As Parameter = GetParameter(oAssemblyDoc, "ParameterName")</code>
        /// </summary>
        /// <param name="document"></param>
        /// <param name="parameterName"></param>
        /// <returns></returns>
        public static Parameter GetParameter(this AssemblyDocument document, string parameterName)
            => GetParameter((Document)document, parameterName);

        /// <summary>
        /// Returns a parameter object from a PartDocument object specified by name.
        ///  <code></code>VB sample:<code>Dim oParameter As Parameter = GetParameter(oPartDoc, "ParameterName")</code>
        /// </summary>
        /// <param name="document"></param>
        /// <param name="parameterName"></param>
        /// <returns></returns>
        public static Parameter GetParameter(this PartDocument document, string parameterName)
            => GetParameter((Document)document, parameterName);

        /// <summary>
        /// Returns a parameter object from a DrawingDocument object specified by name.
        ///  <code></code>VB sample:<code>Dim oParameter As Parameter = GetParameter(oDrawingDoc, "ParameterName")</code>
        /// </summary>
        /// <param name="document"></param>
        /// <param name="parameterName"></param>
        /// <returns></returns>
        public static Parameter GetParameter(this DrawingDocument document, string parameterName)
            => GetParameter((Document)document, parameterName);

        /// <summary>
        /// Returns a boolean indicating if a Parameter exists within a Document object.
        /// <code></code>VB sample:<code>Dim answer As Boolean = ParameterExists(oParameter)</code>
        /// </summary>
        /// <param name="parameter">Inventor.Parameter</param>
        /// <returns>Boolean</returns>
        private static bool ParameterExists(Parameter parameter)
        {
            return !(parameter is null);
        }

        /// <summary>
        /// Returns the Parameters object in a specified Document object.
        /// <code></code>VB sample:<code>Dim oParams As Parameters = GetParameters(oDoc)</code>
        /// </summary>
        /// <param name="document">Inventor.Document</param>
        /// <returns>Parameters</returns>
        public static Parameters GetParameters(this Document document)
        {
            switch (document)
            {
                case PartDocument part:
                    return GetParameters(part);

                case AssemblyDocument assembly:
                    return GetParameters(assembly);

                case DrawingDocument drawing:
                    return GetParameters(drawing);

                default:
                    return null;
            }
        }

        /// <summary>
        /// Returns the Parameters object in a specified AssemblyDocument object.
        /// <code></code>VB sample:<code>Dim oParams As Parameters = GetParameters(oAssemblyDoc)</code>
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public static Parameters GetParameters(this AssemblyDocument document)
            => document.ComponentDefinition.Parameters;

        /// <summary>
        /// Returns the Parameters object in a specified PartDocument object.
        /// <code></code>VB sample:<code>Dim oParams As Parameters = GetParameters(oPartDoc)</code>
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public static Parameters GetParameters(this PartDocument document)
            => document.ComponentDefinition.Parameters;

        /// <summary>
        /// Returns the Parameters object in a specified DrawingDocument object.
        /// <code></code>VB sample:<code>Dim oParams As Parameters = GetParameters(oDrawingDoc)</code>
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public static Parameters GetParameters(this DrawingDocument document)
            => document.Parameters;

        /// <summary>
        /// Enumerate all parameter objects in a provided Document object as IEnumerable{Parameter}
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public static IEnumerable<Parameter> EnumerateParameters(this Document document)
        {
            Parameters parameters = document.GetParameters();
            foreach (Parameter parameter in parameters)
            {
                yield return parameter;
            }
        }

        /// <summary>
        /// Enumerate all parameter objects in a provided AssemblyDocument object as IEnumerable{Parameter}
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public static IEnumerable<Parameter> EnumerateParameters(this AssemblyDocument document)
        {
            Parameters parameters = document.GetParameters();
            foreach (Parameter parameter in parameters)
            {
                yield return parameter;
            }
        }

        /// <summary>
        /// Enumerate all parameter objects in a provided PartDocument object as IEnumerable{Parameter}
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public static IEnumerable<Parameter> EnumerateParameters(this PartDocument document)
        {
            Parameters parameters = document.GetParameters();
            foreach (Parameter parameter in parameters)
            {
                yield return parameter;
            }
        }

        /// <summary>
        /// Enumerate all parameter objects in a provided DrawingDocument object as IEnumerable{Parameter}
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public static IEnumerable<Parameter> EnumerateParameters(this DrawingDocument document)
        {
            Parameters parameters = document.GetParameters();
            foreach (Parameter parameter in parameters)
            {
                yield return parameter;
            }
        }

        /// <summary>
        /// Tests if the provided parameter is writable.  Only kModelParameters and kUserParameters return true.
        /// <code></code>VB sample:<code>Dim answer As Boolean = ParameterIsWritable(oParameter)</code>
        /// </summary>
        /// <param name="parameter">Parameter</param>
        /// <returns>Boolean</returns>
        public static bool ParameterIsWritable(this Parameter parameter)
        {
            if (parameter.ParameterType == ParameterTypeEnum.kModelParameter || parameter.ParameterType == ParameterTypeEnum.kUserParameter)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Tests if the parameter matching the provided parameterName is writable.  Only kModelParameters and kUserParameters return true.
        /// <code></code>VB sample:<code>Dim answer As Boolean = ParameterIsWritable(oDoc, "ThisParameterName")</code>
        /// </summary>
        /// <param name="document">Document</param>
        /// <param name="parameterName">String</param>
        /// <returns>Boolean</returns>
        public static bool ParameterIsWritable(Document document, string parameterName)
        {
            Parameter parameter;
            try
            {
                parameter = GetParameter(document, parameterName);

                if (parameter.ParameterType == ParameterTypeEnum.kModelParameter || parameter.ParameterType == ParameterTypeEnum.kUserParameter)
                {
                    return true;
                }
            }
            catch
            { }

            return false;
        }

        /// <summary>
        /// Tests if the parameter matching the provided parameterName is writable.  Only kModelParameters and kUserParameters return true.
        /// <code></code>VB sample:<code>Dim answer As Boolean = ParameterIsWritable(oAssemblyDoc, "ThisParameterName")</code>
        /// </summary>
        /// <param name="document"></param>
        /// <param name="parameterName"></param>
        /// <returns></returns>
        public static bool ParameterIsWritable(AssemblyDocument document, string parameterName)
            => ParameterIsWritable(document, parameterName);

        /// <summary>
        /// Tests if the parameter matching the provided parameterName is writable.  Only kModelParameters and kUserParameters return true.
        /// <code></code>VB sample:<code>Dim answer As Boolean = ParameterIsWritable(oPartDoc, "ThisParameterName")</code>
        /// </summary>
        /// <param name="document"></param>
        /// <param name="parameterName"></param>
        /// <returns></returns>
        public static bool ParameterIsWritable(PartDocument document, string parameterName)
            => ParameterIsWritable(document, parameterName);

        /// <summary>
        /// Tests if the parameter matching the provided parameterName is writable.  Only kModelParameters and kUserParameters return true.
        /// <code></code>VB sample:<code>Dim answer As Boolean = ParameterIsWritable(oDrawingDoc, "ThisParameterName")</code>
        /// </summary>
        /// <param name="document"></param>
        /// <param name="parameterName"></param>
        /// <returns></returns>
        public static bool ParameterIsWritable(DrawingDocument document, string parameterName)
            => ParameterIsWritable(document, parameterName);

        /// <summary>
        /// Return a list of parameter names within the specified Document.
        /// </summary>
        /// <param name="document">Inventor.Document</param>
        /// <returns>List (of string)</returns>
        public static List<string> GetParameterNames(this Document document)
        {
            switch (document.DocumentType)
            {
                case DocumentTypeEnum.kPartDocumentObject:
                    return GetParameterNames((PartDocument)document);

                case DocumentTypeEnum.kAssemblyDocumentObject:
                    return GetParameterNames((AssemblyDocument)document);

                case DocumentTypeEnum.kDrawingDocumentObject:
                    return GetParameterNames((DrawingDocument)document);

                default:
                    throw new Exception("Unknown type of document passed to GetParameterNames");
            }
        }

        /// <summary>
        /// Return a list of parameter names within the specified AssemblyDocument.
        /// </summary>
        /// <param name="document"></param>
        /// <returns>List (of string)</returns>
        public static List<string> GetParameterNames(this AssemblyDocument document)
        {
            Parameters parameters = document.ComponentDefinition.Parameters;
            return EnumerateParameterNames(parameters).ToList();
        }

        /// <summary>
        /// Return a list of parameter names within the specified PartDocument.
        /// </summary>
        /// <param name="document"></param>
        /// <returns>List (of string)</returns>
        public static List<string> GetParameterNames(this PartDocument document)
        {
            Parameters parameters = document.ComponentDefinition.Parameters;
            return EnumerateParameterNames(parameters).ToList();
        }

        /// <summary>
        /// Return a list of parameter names within the specified DrawingDocument.
        /// </summary>
        /// <param name="document"></param>
        /// <returns>List (of string)</returns>
        public static List<string> GetParameterNames(this DrawingDocument document)
        {
            Parameters parameters = document.Parameters;
            return EnumerateParameterNames(parameters).ToList();
        }

        /// <summary>
        /// Return a list of parameter names within the specified Parameters object.
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns>List (of string)</returns>
        public static IEnumerable<string> EnumerateParameterNames(Parameters parameters)
        {
            foreach (Parameter parameter in parameters)
                yield return parameter.Name;
        }
    }
}