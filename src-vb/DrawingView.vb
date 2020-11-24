Imports Inventor

Public Class DrawingView

    Public View As Inventor.DrawingView

    Public Sub New(view As Inventor.DrawingView)
        view = view
    End Sub

    'TODO: make these set as well as get?
    Public Function UpperLeft() As Inventor.Point2d
        Dim transientGeometry As Inventor.TransientGeometry = ThisApplication.TransientGeometry

        Dim x As Double = View.Left
        Dim y As Double = View.Top
        Dim point As Inventor.Point2d = transientGeometry.CreatePoint2d(x, y)
        Return point

    End Function

    Public Function UpperRight() As Inventor.Point2d
        Dim transientGeometry As Inventor.TransientGeometry = ThisApplication.TransientGeometry

        Dim x As Double = View.Left + View.Width
        Dim y As Double = View.Top
        Dim point As Inventor.Point2d = transientGeometry.CreatePoint2d(x, y)
        Return point

    End Function

    Public Function LowerLeft() As Inventor.Point2d
        Dim transientGeometry As Inventor.TransientGeometry = ThisApplication.TransientGeometry

        Dim x As Double = View.Left
        Dim y As Double = View.Top + View.Height
        Dim point As Inventor.Point2d = transientGeometry.CreatePoint2d(x, y)
        Return point

    End Function
    Public Function LowerRight() As Inventor.Point2d
        Dim transientGeometry As Inventor.TransientGeometry = ThisApplication.TransientGeometry

        Dim x As Double = View.Left + View.Width
        Dim y As Double = View.Top + View.Height
        Dim point As Inventor.Point2d = transientGeometry.CreatePoint2d(x, y)
        Return point
    End Function



End Class
