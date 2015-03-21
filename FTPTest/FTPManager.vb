Imports System.IO
Imports System.Net

Public Class FTPManager
    Dim FTPIP = "ftp://127.0.0.1/"

    Public Sub New()
        MakeFile(FTPIP & "leerlingen")
        MakeFile(FTPIP & "Bedrijven")
    End Sub

    Public Sub MakeFile(ByVal uri)
        Dim myFtpResponse As FtpWebResponse
        If (Not ftpfolderExists(uri)) Then
            Dim Request As FtpWebRequest = CType(FtpWebRequest.Create(uri), FtpWebRequest)
            With Request
                .Credentials = New NetworkCredential("Program", "LXEE8=Yp3jP(-5|")
                .Method = WebRequestMethods.Ftp.MakeDirectory
            End With

            myFtpResponse = CType(Request.GetResponse(), FtpWebResponse)
        End If
    End Sub

    Private Function ftpfolderExists(ByVal URI)
        Dim Request = DirectCast(WebRequest.Create(URI), FtpWebRequest)

        With Request
            .Credentials = New NetworkCredential("Program", "LXEE8=Yp3jP(-5|")
            .Method = WebRequestMethods.Ftp.ListDirectory
        End With

        Try
            Using response As FtpWebResponse = DirectCast(Request.GetResponse(), FtpWebResponse)
                MsgBox("exists!")
            End Using

        Catch ex As WebException
            Dim response As FtpWebResponse = DirectCast(ex.Response, FtpWebResponse)

            If response.StatusCode = FtpStatusCode.ActionNotTakenFileUnavailable Then
                MsgBox("Doesn't exist!")
                Return False
            End If
        End Try
        Return True
    End Function


    Private Function FtpFileExists(ByVal URI As String) As Boolean
        Dim request As FtpWebRequest = WebRequest.Create(URI)
        With request
            .Credentials = New NetworkCredential("Program", "LXEE8=Yp3jP(-5|")
            .Method = WebRequestMethods.Ftp.GetFileSize
        End With

        Try
            Dim response As FtpWebResponse = request.GetResponse()
            response.Close()
        Catch ex As WebException
            Dim response As FtpWebResponse = ex.Response
            If FtpStatusCode.ActionNotTakenFileUnavailable = response.StatusCode Then
                response.Close()
                Return False
            End If
        End Try
        Return True
    End Function

    Public Sub OpenFileBrowser(ByVal URI As String)
        Process.Start("explorer.exe", URI)
    End Sub

    Public Sub RemoveFile(ByVal URI As String)
        Dim request As FtpWebRequest = WebRequest.Create(URI)

        With request
            .Credentials = New System.Net.NetworkCredential("Program", "LXEE8=Yp3jP(-5|")
            .Method = WebRequestMethods.Ftp.DeleteFile
        End With
        Try
            Dim Response As FtpWebResponse = CType(request.GetResponse(), FtpWebResponse)
            Response = request.GetResponse()
            Response.Close()
        Catch ex As Exception
            MsgBox(ex)
        End Try

    End Sub

End Class
