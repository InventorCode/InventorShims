Imports InventorShims
Imports Inventor
AddVbFile "src-vb/PathShim.vb"

'Public Module Test


Sub Main()
    Dim test As String
    Dim testA As String = "C:\A\Test\String\"
    Dim testB As String = PathShim.UpOneLevel(testA)
    Logger.info("UpOneLevel Test 1 : " & If(testB.Equals("C:\A\Test\"), "Pass", "Fail") & " : " & testB)


    testA = "C:\A\Test\String"
    testB = PathShim.UpOneLevel(testA)
    Logger.info("UpOneLevel Test 2 : " & If(testB.Equals("C:\A\Test\"), "Pass", "Fail") & " : " & testB)


    testA = "C:"
    testB = PathShim.UpOneLevel(testA)
    Logger.info("UpOneLevel Test 3 : " & If(Equals(testB, Nothing), "Pass", "Fail") & " : Nothing")


    testA = "C:\"
    testB = PathShim.UpOneLevel(testA)
    Logger.info("UpOneLevel Test 4 : " & If(Equals(testB, Nothing), "Pass", "Fail") & " : Nothing")


    testA = "C:\A\"
    testB = PathShim.UpOneLevel(testA)
    Logger.info("UpOneLevel Test 5 : " & If(Equals(testB, "C:\"), "Pass", "Fail") & " : " & testB)


    testA = "C\x"
    testB = PathShim.UpOneLevel(testA)
    Logger.info("UpOneLevel Test 6 : " & If(Equals(testB, "C\"), "Pass", "Fail") & " : " & testB)



    test = "C:\!Engineering Admin\CAD\Inventor Library\"
    Logger.info("IsLibraryPath Test 1 : " & If(PathShim.IsLibraryPath(test, ThisApplication) = True, "Pass", "Fail") & " : " & test)

    test = "C:\!Engineering Admin\CAD\Inventor Library"
    Logger.info("IsLibraryPath Test 2 : " & If(PathShim.IsLibraryPath(test, ThisApplication) = True, "Pass", "Fail") & " : " & test)

    test = "C:\!Engineering Admin\CAD\Inventor\"
    Logger.info("IsLibraryPath Test 3 : " & If(PathShim.IsLibraryPath(test, ThisApplication) = False, "Pass", "Fail") & " : " & test)

    test = "C:\!Engineering Admin\CAD\Inventor Library\"
    Logger.info("IsContentCenterPath Test 1 : " & If(PathShim.IsContentCenterPath(test, ThisApplication) = False, "Pass", "Fail") & " : " & test)

    test = "Z:\Inventor\_Content Center\"
    Logger.info("IsContentCenterPath Test 2 : " & If(PathShim.IsContentCenterPath(test, ThisApplication) = True, "Pass", "Fail") & " : " & test)

    test = "Z:\Inventor\_Content Center"
    Logger.info("IsContentCenterPath Test 3 : " & If(PathShim.IsContentCenterPath(test, ThisApplication) = True, "Pass", "Fail") & " : " & test)

    test = "123"
    Logger.info("IsContentCenterPath Test 4 : " & If(PathShim.IsContentCenterPath(test, ThisApplication) = False, "Pass", "Fail") & " : " & test)

    test = "C:\!Engineering Admin\CAD\Inventor Library\"
    Logger.info("TrimTrailingSlash Test 1 : " & If(PathShim.TrimTrailingSlash(test) = "C:\!Engineering Admin\CAD\Inventor Library", "Pass", "Fail"))

    test = "C:\!Engineering Admin\CAD\Inventor Library"
    Logger.info("TrimTrailingSlash Test 2 : " & If(PathShim.TrimTrailingSlash(test) = "C:\!Engineering Admin\CAD\Inventor Library", "Pass", "Fail"))


End Sub
'End Module