AddVbFile "PropertyShim.vb"

'Test the PropertyShim SetProperty function from inventor's ilogic environment.
Sub Main()
    Dim oDoc As Inventor.Document = ThisApplication.ActiveDocument
    PropertyShim.SetProperty(oDoc, "Title", "Stuffffff")
    PropertyShim.SetProperty(oDoc, "Things", "Stuffffff2")

    MessageBox.Show(PropertyShim.GetProperty(oDoc, "Title"))
    MessageBox.Show(PropertyShim.GetProperty(oDoc, "Things"))


End Sub