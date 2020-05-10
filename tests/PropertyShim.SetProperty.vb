Imports InventorShims
Imports Inventor
AddVbFile "src/PropertyShim.vb"

'Test the PropertyShim SetProperty function from inventor's ilogic environment.
'Module Test
    Sub Main()
        Dim oDoc As Inventor.Document = ThisApplication.ActiveDocument
        PropertyShim.SetProperty(oDoc, "Title", "Stuffffff")
        PropertyShim.SetProperty(oDoc, "Things", "Stuffffff2")

        PropertyShim.SetProperty(oDoc, "Custom Prop Set", "Things", "Stufzzzzzz")

        MessageBox.Show(PropertyShim.GetProperty(oDoc, "Title"))
        MessageBox.Show(PropertyShim.GetProperty(oDoc, "Things"))
        MessageBox.Show(PropertyShim.GetProperty(oDoc, "Custom Prop Set", "Things"))

    End Sub
'End Module

