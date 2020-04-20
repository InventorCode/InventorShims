'Imports System.Runtime.CompilerServices
Imports Inventor

Public Class IpropertyShim

        Shared ReadOnly PropertyLookup As Dictionary(Of String, String) = New Dictionary(Of String,String)(StringComparer.OrdinalIgnoreCase) From 
        {{"Title", "Inventor Summary Information"},
        {"Subject", "Inventor Summary Information"},
        {"Author", "Inventor Summary Information"},
        {"Keywords", "Inventor Summary Information"},
        {"Comments", "Inventor Summary Information"},
        {"Last Saved By", "Inventor Summary Information"},
        {"Revision Number", "Inventor Summary Information"},
        {"Thumbnail", "Inventor Summary Information"},
        {"Category","Inventor Document Summary Information"},
        {"Manager","Inventor Document Summary Information"},
        {"Company","Inventor Document Summary Information"},
        {"Creation Time","Design Tracking Properties"},
        {"Part Number","Design Tracking Properties"},
        {"Project","Design Tracking Properties"},
        {"Cost Center","Design Tracking Properties"},
        {"Checked By","Design Tracking Properties"},
        {"Date Checked","Design Tracking Properties"},
        {"Engr Approved By","Design Tracking Properties"},
        {"Engr Date Approved","Design Tracking Properties"},
        {"User Status","Design Tracking Properties"},
        {"Material","Design Tracking Properties"},
        {"Part Property Revision Id","Design Tracking Properties"},
        {"Catalog Web Link","Design Tracking Properties"},
        {"Part Icon","Design Tracking Properties"},
        {"Description","Design Tracking Properties"},
        {"Vendor","Design Tracking Properties"},
        {"Document SubType","Design Tracking Properties"},
        {"Document SubType Name","Design Tracking Properties"},
        {"Proxy Refresh Date","Design Tracking Properties"},
        {"Mfg Approved By","Design Tracking Properties"},
        {"Mfg Date Approved","Design Tracking Properties"},
        {"Cost","Design Tracking Properties"},
        {"Standard","Design Tracking Properties"},
        {"Design Status","Design Tracking Properties"},
        {"Designer","Design Tracking Properties"},
        {"Engineer","Design Tracking Properties"},
        {"Authority","Design Tracking Properties"},
        {"Parameterized Template","Design Tracking Properties"},
        {"Template Row","Design Tracking Properties"},
        {"External Property Revision Id","Design Tracking Properties"},
        {"Standard Revision","Design Tracking Properties"},
        {"Manufacturer","Design Tracking Properties"},
        {"Standards Organization","Design Tracking Properties"},
        {"Language","Design Tracking Properties"},
        {"Defer Updates","Design Tracking Properties"},
        {"Size Designation","Design Tracking Properties"},
        {"Categories","Design Tracking Properties"},
        {"Stock Number","Design Tracking Properties"},
        {"Weld Material","Design Tracking Properties"},
        {"Mass","Design Tracking Properties"},
        {"SurfaceArea","Design Tracking Properties"},
        {"Volume","Design Tracking Properties"},
        {"Density","Design Tracking Properties"},
        {"Valid MassProps","Design Tracking Properties"},
        {"Flat Pattern Width","Design Tracking Properties"},
        {"Flat Pattern Length","Design Tracking Properties"},
        {"Flat Pattern Area","Design Tracking Properties"},
        {"Sheet Metal Rule","Design Tracking Properties"},
        {"Last Updated With","Design Tracking Properties"},
        {"Sheet Metal Width","Design Tracking Properties"},
        {"Sheet Metal Length","Design Tracking Properties"},
        {"Sheet Metal Area","Design Tracking Properties"},
        {"Material Identifier","Design Tracking Properties"},
        {"Appearance","Design Tracking Properties"},
        {"Flat Pattern Defer Update","Design Tracking Properties"}}



''' <summary>
''' Return the specified document property's value.  Only requires a document and property name. 
''' </summary>
''' <param name="doc"></param>
''' <param name="key"></param>
''' <returns></returns>
    Shared Function GetProperty(ByRef doc As Inventor.Document, ByVal key As String) As Object

        Dim value As String
        If PropertyLookup.TryGetValue(key, value) then
            return doc.PropertySets.Item(value).Item(key).Value
        End If

        'Not found in standard properties, search custom properties
        Dim customPropertySet As Inventor.PropertySet = doc.PropertySets.Item("Inventor User Defined Properties")
        Dim items as Integer() = {}
        Dim names As String() = {}
        Dim values As Object() = {}

        customPropertySet.GetPropertyInfo(items,names,values)

        If names.Contains(key, StringComparer.OrdinalIgnoreCase)
            return customPropertySet.Item(key).Value
        End If
        'TODO: If still not found, search other custom property sets!

        If doc.PropertySets.Count >= 5 Then
            'go through the custom ones...
        End If

        'Still not found, return nothing...
        return ""

    End Function

    Shared Sub SetProperty(ByRef doc As Inventor.Document, ByVal key As String, ByVal value as Object)
        
    End Sub

End Class