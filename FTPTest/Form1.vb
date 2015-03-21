Imports System.IO
Imports System.Net
Public Class Form1
    Dim ftpManager As New FTPManager
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        OpenFileDialog1.ShowDialog()
        TextBox1.Text = OpenFileDialog1.FileName
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        Dim ftpRequest As FtpWebRequest
        ftpRequest = DirectCast(WebRequest.Create("ftp://Test@127.0.0.1/Bedrijven" & "/" & Path.GetFileName(TextBox1.Text)), WebRequest)

        With ftpRequest
            .Credentials = New NetworkCredential("Test", "")
            .Method = WebRequestMethods.Ftp.UploadFile
        End With

        Try
            Dim bFile() As Byte = System.IO.File.ReadAllBytes(TextBox1.Text)

            Dim clsStream As Stream = ftpRequest.GetRequestStream()
            clsStream.Write(bFile, 0, bFile.Length)
            clsStream.Close()
            clsStream.Dispose()
            MsgBox("Bestand is geupload")
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try

    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Dim myFtpResponse As FtpWebResponse
        Dim ftprequest As FtpWebRequest = CType(FtpWebRequest.Create("ftp://Test@127.0.0.1/Leerlingen/Balder/"), FtpWebRequest)
        ftprequest.Credentials = New NetworkCredential("Test", "")
        ftprequest.Method = WebRequestMethods.Ftp.MakeDirectory
        myFtpResponse = CType(ftprequest.GetResponse(), FtpWebResponse)
    End Sub

    Private Sub TabControl1_Selected(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TabControlEventArgs) Handles TabControl1.Selected
        Process.Start("explorer.exe", "ftp://@administrator127.0.0.1/")
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click

        Dim ftpRequest As FtpWebRequest
        ftpRequest = DirectCast(WebRequest.Create("ftp://Test@127.0.0.1/"), WebRequest)

        With ftpRequest
            .Credentials = New NetworkCredential("Test", "")
            .Method = WebRequestMethods.Ftp.ListDirectoryDetails
        End With

        Try
            Dim ftpResponse As FtpWebResponse = CType(ftpRequest.GetResponse(), FtpWebResponse)
            Dim responsestream As Stream = ftpResponse.GetResponseStream()
            Dim reader As StreamReader = New StreamReader(responsestream)
            MsgBox(reader.ReadToEnd())

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ftpManager.RemoveFile("ftp://127.0.0.1/leerlingen/michieltje.png")
    End Sub
End Class
