Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports System.Net
Imports System.IO
Imports System.Data.OleDb


Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'Dim strAPIURL As String = "http://us.battle.net/api/wow/auction/data/" + txtRealm.Text
        'Dim request As WebRequest = WebRequest.Create(strAPIURL) ' Create a request for the URL. 
        'Dim response As WebResponse = request.GetResponse()   ' Get the response.
        'MessageBox.Show(CType(response, HttpWebResponse).StatusDescription)     ' Display the status.
        'Dim dataStream As Stream = response.GetResponseStream() ' Get the stream containing content returned by the server.
        'Dim reader As New StreamReader(dataStream) ' Open the stream using a StreamReader for easy access.
        'Dim responseFromServer As String = reader.ReadToEnd()   ' Read the content.

        ' Display the content
        'Dim result = JsonConvert.DeserializeObject(Of APIResult)(responseFromServer)
        'MessageBox.Show(result.files(0).url)

        ' Clean up the stream and response
        'reader.Close()
        'response.Close()

        ' Gather AH Data
        'Dim AHDataURL As String = result.files(0).url
        'Dim AHDateModified As String = result.files(0).lastModified

        ' request = WebRequest.Create(AHDataURL)
        ' response = request.GetResponse()
        ' dataStream = response.GetResponseStream()
        ' reader = New StreamReader(dataStream)
        ' Dim json As String = reader.ReadToEnd()


        '        Dim items = JsonConvert.DeserializeObject(Of AuctionResult1())(json)
        '        Dim mylist = items.ToArray
        '        MessageBox.Show(items(0).auctions(0).auctions(0).item(0))

        Dim remoteUri As String = "http://www.wowuction.com/us/blackrock/horde/Tools/RealmDataExportGetFileStatic?token=Oq_4EsX7G1zjuxD7Fits0w2"
        Dim myUri As Uri = New Uri(remoteUri)

        Dim mywebrequest As WebRequest = WebRequest.Create(remoteUri)

        Dim filename As String = "WoWuActionDataExport.csv"
        Dim myStringWebResource As String = Nothing
        Dim myWebClient As New WebClient()

        AddHandler myWebClient.DownloadProgressChanged, AddressOf mywebclient_ProgressChanged
        AddHandler myWebClient.DownloadFileCompleted, AddressOf mywebclient_DownloadCompleted

        Dim arr() As Byte = myWebClient.DownloadData(myUri)
        myWebClient.DownloadFileAsync(New Uri(remoteUri), filename)
        Button1.Text = "Download in Progress"
        Button1.Enabled = False



        Dim folder = "WoWUactionDataExport.csv"
        Dim CnStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & folder & ";Extended Properties=""text;HDR=No;FMT=Delimited"";"
        Dim dt As New DataTable
        Using Adp As New OleDbDataAdapter("select * from [nos.csv]", CnStr)
            Adp.Fill(dt)
        End Using








    End Sub

    Private Sub mywebclient_ProgressChanged(ByVal sender As Object, ByVal e As DownloadProgressChangedEventArgs)
        Dim bytesIn As Double = Double.Parse(e.BytesReceived.ToString())
        Dim totalBytes As Double = Double.Parse(e.TotalBytesToReceive.ToString())
        Dim percentage As Double = bytesIn / totalBytes * 100

        ProgressBar1.Value = Int32.Parse(Math.Truncate(percentage).ToString())

    End Sub

    Private Sub mywebclient_DownloadCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.AsyncCompletedEventArgs)
        MessageBox.Show("Download Complete")
        Button1.Text = "Start Download"
        Button1.Enabled = True

    End Sub

    Public Function GetFileNameFromURL(ByVal URL As String) As String
        Try
            Return URL.Substring(URL.LastIndexOf("/") + 1)
        Catch ex As Exception
            Return URL
        End Try
    End Function

End Class

