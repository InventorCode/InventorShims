'Imports InventorShims
Imports Inventor
AddVbFile "src-vb/iProperty.vb"

'Test the PropertyShim SetProperty function from inventor's ilogic environment.
'Module Test
    Sub Main()
        Dim oDoc As Inventor.Document = ThisApplication.ActiveDocument
        iProperty.SetProperty(oDoc, "Title", "Stuffffff")
        iProperty.SetProperty(oDoc, "Things", "Stuffffff2")

        iProperty.SetProperty(oDoc, "Custom Prop Set", "Things", "Stufzzzzzz")

        MessageBox.Show(iProperty.GetProperty(oDoc, "Title"))
        MessageBox.Show(iProperty.GetProperty(oDoc, "Things"))
        MessageBox.Show(iProperty.GetProperty(oDoc, "Custom Prop Set", "Things"))

    End Sub
'End Module

