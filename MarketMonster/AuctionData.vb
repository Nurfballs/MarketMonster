Public Class APIData
    Public Property url() As String
        Get
            Return m_url
        End Get
        Set(value As String)
            m_url = value
        End Set
    End Property
    Private m_url As String

    Public Property lastModified() As String
        Get
            Return m_lastModified
        End Get
        Set(value As String)
            m_lastModified = value
        End Set

    End Property
    Private m_lastModified As String
End Class

Public Class APIResult
    Public files As List(Of APIData)
End Class

Public Class AuctionResult1
    Public Property [auctions] As AuctionResult2()
End Class

Public Class AuctionResult2
    Public Property [auctions] As AuctionItem()
End Class

Public Class AuctionItem
    Public Property item() As String
        Get
            Return m_item
        End Get
        Set(value As String)
            m_item = value
        End Set
    End Property
    Private m_item As String
    Public Property owner() As String
        Get
            Return m_owner
        End Get
        Set(value As String)
            m_owner = value
        End Set
    End Property
    Private m_owner
End Class

Public Class JsonStringToDataTable
    Public Property JsonString As String
    Public Property DataTable As DataTable

    Private mErrorMessage As String = ""
    Public ReadOnly Property ErrorMessage As String
        Get
            Return mErrorMessage
        End Get
    End Property

    Public Function Convert() As Boolean
        Try
            Me.DataTable = Newtonsoft.Json.JsonConvert.DeserializeObject(Of DataTable)(Me.JsonString)
            Return True

        Catch ex As Exception
            mErrorMessage = ex.Message
            Return False
        End Try
    End Function

End Class