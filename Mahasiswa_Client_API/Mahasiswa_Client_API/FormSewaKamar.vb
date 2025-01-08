Imports System.Text
Imports System.Net.Http
Imports Newtonsoft.Json
Imports System.Net
Public Class FormSewaKamar
    Private Sub TambahData()

        ' Set the data you want to send as key-value pairs
        Dim id_kamar As String = txtKAMAR.Text
        Dim id_penyewa As String = txtPENYEWA.Text
        Dim tanggal_checkin As String = txtMASUK.Text
        Dim tanggal_checkout As String = txtKELUAR.Text
        Dim jumlah_malam As String = txtJUMBLAH.Text
        Dim total_biaya As String = txtBIAYA.Text

        Dim postData As String = $"id_kamar={id_kamar}&id_penyewa={id_penyewa}&tanggal_checkin={tanggal_checkin}&tanggal_checkout={tanggal_checkout}&jumblah_malam={jumlah_malam}&total_biaya={total_biaya}"

        ' Create a new WebClient instance
        Dim client As New WebClient()

        ' Set the content type header
        client.Headers.Add("Content-Type", "application/x-www-form-urlencoded")

        Try
            ' Encode the data as a byte array
            Dim byteArray As Byte() = Encoding.ASCII.GetBytes(postData)

            ' Send the POST request and get the response
            Dim responseBytes As Byte() = client.UploadData(sewakamar_api, "POST", byteArray)

            ' Convert the response bytes to a string
            Dim responseBody As String = Encoding.ASCII.GetString(responseBytes)

            ' Display the response
            MessageBox.Show(responseBody, "Response")
        Catch ex As Exception
            ' Handle any errors that occur during the request
            MessageBox.Show("An error occurred: " & ex.Message, "Error")
        End Try
        GetClear()
    End Sub

    Private Sub UpdateData()

        ' Set the data you want to send as key-value pairs
        Dim id_kamar As String = txtKAMAR.Text
        Dim id_penyewa As String = txtPENYEWA.Text
        Dim tanggal_checkin As String = txtMASUK.Text
        Dim tanggal_checkout As String = txtKELUAR.Text
        Dim jumlah_malam As String = txtJUMBLAH.Text
        Dim total_biaya As String = txtBIAYA.Text

        Dim postData As String = $"id_kamar={id_kamar}&id_penyewa={id_penyewa}&tanggal_checkin={tanggal_checkin}&tanggal_checkout={tanggal_checkout}&jumblah_malam={jumlah_malam}&total_biaya={total_biaya}"

        ' Create a new WebClient instance
        Dim client As New WebClient()

        ' Set the content type header
        client.Headers.Add("Content-Type", "application/x-www-form-urlencoded")

        Try
            ' Encode the data as a byte array
            Dim byteArray As Byte() = Encoding.ASCII.GetBytes(postData)

            ' Send the PUT request and get the response
            Dim responseBytes As Byte() = client.UploadData(sewakamar_api & "?id_kamar=" & txtKAMAR.Text, "PUT", byteArray)

            ' Convert the response bytes to a string
            Dim responseBody As String = Encoding.ASCII.GetString(responseBytes)

            ' Display the response
            MessageBox.Show(responseBody, "Response")
        Catch ex As Exception
            ' Handle any errors that occur during the request
            MessageBox.Show("An error occurred: " & ex.Message, "Error")
        End Try
        GetClear()
    End Sub

    Private Sub DeleteData()

        ' Set the data you want to send as key-value pairs
        Dim id_kamar As String = txtKAMAR.Text
        Dim id_penyewa As String = txtPENYEWA.Text
        Dim tanggal_checkin As String = txtMASUK.Text
        Dim tanggal_checkout As String = txtKELUAR.Text
        Dim jumlah_malam As String = txtJUMBLAH.Text
        Dim total_biaya As String = txtBIAYA.Text

        Dim postData As String = $"id_kamar={id_kamar}&id_penyewa={id_penyewa}&tanggal_checkin={tanggal_checkin}&tanggal_checkout={tanggal_checkout}&jumblah_malam={jumlah_malam}&total_biaya={total_biaya}"

        ' Create a new WebClient instance
        Dim client As New WebClient()

        ' Set the content type header
        client.Headers.Add("Content-Type", "application/x-www-form-urlencoded")

        Try
            ' Encode the data as a byte array
            Dim byteArray As Byte() = Encoding.ASCII.GetBytes(postData)

            ' Send the PUT request and get the response
            Dim responseBytes As Byte() = client.UploadData(sewakamar_api & "?id_kamar=" & txtKAMAR.Text, "DELETE", byteArray)

            ' Convert the response bytes to a string
            Dim responseBody As String = Encoding.ASCII.GetString(responseBytes)

            ' Display the response
            MessageBox.Show(responseBody, "Response")
        Catch ex As Exception
            ' Handle any errors that occur during the request
            MessageBox.Show("An error occurred: " & ex.Message, "Error")
        End Try
        GetClear()
    End Sub

    Private Sub GetData()
        Using client As New HttpClient()
            ' Send a GET request to the API endpoint
            Dim response As HttpResponseMessage = client.GetAsync(sewakamar_api & "?id_kamar=" & txtKAMAR.Text).Result

            ' Check if the request was successful
            If response.IsSuccessStatusCode Then
                ' Read the response content as a string
                Dim jsonString As String = response.Content.ReadAsStringAsync().Result
                Try
                    If (jsonString = "[]") Then
                        sewakamar_baru = True
                        MessageBox.Show("Data Not Found")
                        Exit Sub
                    Else
                        sewakamar_baru = False
                    End If

                    ' Deserialize the JSON into objects
                    Dim data As List(Of SewaKamar) = JsonConvert.DeserializeObject(Of List(Of SewaKamar))(jsonString)

                    ' Find the matching data based on id_kamar
                    Dim selectedData = data.FirstOrDefault(Function(item) item.id_kamar = txtKAMAR.Text)

                    If selectedData IsNot Nothing Then
                        txtPENYEWA.Text = selectedData.id_penyewa
                        txtMASUK.Text = selectedData.tanggal_checkin
                        txtKELUAR.Text = selectedData.tanggal_checkout
                        txtJUMBLAH.Text = selectedData.jumlah_malam
                        txtBIAYA.Text = selectedData.total_biaya
                    Else
                        MessageBox.Show("Data Not Found")
                        GetClear()
                        Exit Sub
                    End If

                Catch ex As Exception
                    ' Handle any errors that occur during the request
                    MessageBox.Show("An error occurred: " & ex.Message, "Error")
                End Try
            Else
                ' Request failed, handle the error
                MessageBox.Show($"Error: {response.StatusCode} - {response.ReasonPhrase}")
            End If
        End Using
    End Sub

    Private Sub GetClear()
        txtKAMAR.Clear()
        txtPENYEWA.Clear()
        txtMASUK.Clear()
        txtKELUAR.Clear()
        txtJUMBLAH.Clear()
        txtBIAYA.Clear()
        Reloaded()
        txtKAMAR.Focus()
    End Sub
    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        If (sewakamar_baru = True) Then
            TambahData()
        Else
            UpdateData()
        End If
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        GetClear()
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If (sewakamar_baru = False) Then
            Dim result As DialogResult = MessageBox.Show("Apakah data akan dihapus?", "Confirmation", MessageBoxButtons.YesNo)
            If result = DialogResult.Yes Then
                DeleteData()
            Else
                MessageBox.Show("Data batal dihapus.")
            End If
        End If
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Reloaded()
    End Sub

    Private Sub Reloaded()

        ' Create a WebClient instance to make the HTTP request
        Dim client As New WebClient()

        ' Make the GET request and retrieve the response
        Dim response As String = client.DownloadString(sewakamar_api)

        ' Deserialize the JSON response into a list of objects
        Dim data As List(Of SewaKamar) = JsonConvert.DeserializeObject(Of List(Of SewaKamar))(response)

        ' Bind the data to the DataGridView
        dgvData.DataSource = data
    End Sub

    Private Sub txtNIM_KeyDown(sender As Object, e As KeyEventArgs) Handles txtKAMAR.KeyDown
        If (e.KeyCode = Keys.Enter) Then
            GetData()
        End If
    End Sub

    Private Sub dgvData_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvData.CellContentClick

    End Sub
End Class