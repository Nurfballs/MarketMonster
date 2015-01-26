Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports System.Net
Imports System.IO
Imports System.Data.OleDb


Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim strAPIURL As String = "http://us.battle.net/api/wow/auction/data/" + txtRealm.Text
        Dim request As WebRequest = WebRequest.Create(strAPIURL) ' Create a request for the URL. 
        Dim response As WebResponse = request.GetResponse()   ' Get the response.
        'MessageBox.Show(CType(response, HttpWebResponse).StatusDescription)     ' Display the status.
        Dim dataStream As Stream = response.GetResponseStream() ' Get the stream containing content returned by the server.
        Dim reader As New StreamReader(dataStream) ' Open the stream using a StreamReader for easy access.
        Dim responseFromServer As String = reader.ReadToEnd()   ' Read the content.

        ' Display the content
        Dim result = JsonConvert.DeserializeObject(Of APIResult)(responseFromServer)
        'MessageBox.Show(result.files(0).url)

        ' Clean up the stream and response
        reader.Close()
        response.Close()

        ' Gather AH Data
        Dim AHDataURL As String = result.files(0).url
        'Dim AHDateModified As String = result.files(0).lastModified

        request = WebRequest.Create(AHDataURL)
        response = request.GetResponse()
        dataStream = response.GetResponseStream()
        reader = New StreamReader(dataStream)
        responseFromServer = reader.ReadToEnd()

        Dim dt As New DataTable
        dt.TableName = "AllAuctions"
        dt.Columns.Add("ItemID")
        dt.Columns.Add("Owner")
        dt.Columns.Add("Bid")
        dt.Columns.Add("Buyout")
        dt.Columns.Add("TimeLeft")

        Dim o As JObject = JObject.Parse(responseFromServer)
        Dim results2 As List(Of JToken) = o.Children().ToList
        For Each item As JProperty In results2
            item.CreateReader()
            Select Case item.Name
                Case "auctions"
                    For Each subitem As JProperty In item.Values
                        Select Case subitem.Name
                            Case "auctions"

                                For Each listing As JObject In subitem.Values
                                    dt.Rows.Add(listing("item"), listing("owner"), listing("bid"), listing("buyout"), listing("timeLeft"))
                                Next


                        End Select
                    Next
                    RichTextBox1.Text = item.Value.ToString
            End Select
        Next

        Dim ds As New DataSet
        ds.Tables.Add(dt)


        Dim dt2 As DataTable = dt.DefaultView.ToTable
        dt2.TableName = "MyAuctions"
        ds.Tables.Add(dt2)
        dt2.DefaultView.RowFilter = "owner = 'Chaosarrow'"

        DataGridView1.DataSource = ds.Tables("AllAuctions")
        GridControl1.DataSource = ds.Tables("MyAuctions")










        'ListBox1.Items.Add()



        '        Dim items = JsonConvert.DeserializeObject(Of AuctionResult1())(json)
        '        Dim mylist = items.ToArray
        '        MessageBox.Show(items(0).auctions(0).auctions(0).item(0))







    End Sub


End Class

