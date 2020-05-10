Imports Inventor

'This can be run in Inventor's ilogic environment.  it will list out the
'iproperties available in a file.

Public Sub Main()
    Dim test As ListIprops = New ListIprops(ThisApplication)
    test.GetPropertySets
'  test.GetInfo

End Sub
Public Class ListIprops

    Private _doc As Document
    Private _file As System.IO.StreamWriter
    Private ReadOnly _epoch As String = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().toString
    Private _path As String

    Sub New(app As Application)
        _doc = app.ActiveDocument
        Dim fullPath As String = _doc.FullFileName
        Dim directory As String = IO.Path.GetDirectoryName(fullPath)
        _path = directory & IO.Path.DirectorySeparatorChar & "IpropertyList " & _epoch & ".txt"

    End Sub

    Private Sub OpenFile
        _file = My.Computer.FileSystem.OpenTextFileWriter(_path, True)
    End Sub
    Private Sub WriteLine(value As String)
        _file.WriteLine(value)
    End Sub
    Private Sub CloseFile
        _file.Close()
    End Sub

    Sub GetPropertySets()
        
        OpenFile
        Dim propertySet As Inventor.PropertySet
        WriteLine("Qty of Property Sets: " & _doc.PropertySets.count)

        For Each propertySet In _doc.PropertySets
            WriteLine("")
            WriteLine(propertySet.Name + " / " + propertySet.InternalName)


            Dim prop As Inventor.Property
            For Each prop In propertySet
                WriteLine(prop.Name + " /" + Str(prop.PropId))
            Next
        Next
        CloseFile
    End Sub

    Sub GetInfo()
        Dim propertySet As Inventor.PropertySet
        dim ids As Integer() = {}
        dim names As String() = {}
        Dim values As Object() = {}
        propertySet = _doc.PropertySets.Item("F29F85E0-4FF9-1068-AB91-08002B27B3D9")
        propertySet.GetPropertyInfo(ids,names,values)

        MsgBox(values(0))
    End Sub

End Class
