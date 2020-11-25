Imports InventorShims
Imports Inventor
AddVbFile "src-vb/PathShim.vb"

'Public Module Test
Sub Main()

    Dim testA = "C:\A\Test\String\"
    Dim testB = PathShim.UpOneLevel(testA)
    Logger.info("UpOneLevel Test 1 : " & If(testB.Equals("C:\A\Test\"), "Pass", "Fail"))
    Logger.info("UpOneLevel Test 1 : " & testB)


    testA = "C:\A\Test\String"
    testB = PathShim.UpOneLevel(testA)
    Logger.info("UpOneLevel Test 2 : " & If(testB.Equals("C:\A\Test\"), "Pass", "Fail"))
    Logger.info("UpOneLevel Test 2 : " & testB)


    testA = "C:"
    testB = PathShim.UpOneLevel(testA)
    Logger.info("UpOneLevel Test 3 : " & If(Equals(testB, Nothing), "Pass", "Fail"))
    Logger.info("UpOneLevel Test 3 : " & testB)


    testA = "C:\"
    testB = PathShim.UpOneLevel(testA)
    Logger.info("UpOneLevel Test 4 : " & If(Equals(testB, Nothing), "Pass", "Fail"))
    Logger.info("UpOneLevel Test 4 : Nothing")


    testA = "C:\A\"
    testB = PathShim.UpOneLevel(testA)
    Logger.info("UpOneLevel Test 5 : " & If(Equals(testB, "C:\"), "Pass", "Fail"))
    Logger.info("UpOneLevel Test 5 : Nothing")

    testA = "C\x"
    testB = PathShim.UpOneLevel(testA)
    Logger.info("UpOneLevel Test 6 : " & If(Equals(testB, "C\"), "Pass", "Fail"))
    Logger.info("UpOneLevel Test 6 : " & testB)

End Sub
'End Module
