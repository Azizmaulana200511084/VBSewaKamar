﻿Imports System.Text
Imports System.Net.Http
Imports Newtonsoft.Json
Imports System.Net
Public Class FormPenyewa
    Private Sub TambahData()

        ' Set the data you want to send as key-value pairs
        Dim nama_penyewa As String = txtNMPENYEWA.Text
        Dim alamat As String = txtALAMAT.Text
        Dim email As String = txtEMAIL.Text
        Dim telepon As String = txtTELEPON.Text

        Dim postData As String = $"nama_penyewa={nama_penyewa}&alamat={alamat}&email={email}&telepon={telepon}"

        ' Create a new WebClient instance
        Dim client As New WebClient()

        ' Set the content type header
        client.Headers.Add("Content-Type", "application/x-www-form-urlencoded")

        Try
            ' Encode the data as a byte array
            Dim byteArray As Byte() = Encoding.ASCII.GetBytes(postData)

            ' Send the POST request and get the response
            Dim responseBytes As Byte() = client.UploadData(penyewa_api, "POST", byteArray)

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
        Dim nama_penyewa As String = txtNMPENYEWA.Text
        Dim alamat As String = txtALAMAT.Text
        Dim email As String = txtEMAIL.Text
        Dim telepon As String = txtTELEPON.Text

        Dim postData As String = $"nama_penyewa={nama_penyewa}&alamat={alamat}&email={email}&telepon={telepon}"

        ' Create a new WebClient instance
        Dim client As New WebClient()

        ' Set the content type header
        client.Headers.Add("Content-Type", "application/x-www-form-urlencoded")

        Try
            ' Encode the data as a byte array
            Dim byteArray As Byte() = Encoding.ASCII.GetBytes(postData)

            ' Send the PUT request and get the response
            Dim responseBytes As Byte() = client.UploadData(penyewa_api & "?nama_penyewa=" & txtNMPENYEWA.Text, "PUT", byteArray)

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
        Dim nama_penyewa As String = txtNMPENYEWA.Text
        Dim alamat As String = txtALAMAT.Text
        Dim email As String = txtEMAIL.Text
        Dim telepon As String = txtTELEPON.Text

        Dim postData As String = $"nama_penyewa={nama_penyewa}&alamat={alamat}&email={email}&telepon={telepon}"

        ' Create a new WebClient instance
        Dim client As New WebClient()

        ' Set the content type header
        client.Headers.Add("Content-Type", "application/x-www-form-urlencoded")

        Try
            ' Encode the data as a byte array
            Dim byteArray As Byte() = Encoding.ASCII.GetBytes(postData)

            ' Send the PUT request and get the response
            Dim responseBytes As Byte() = client.UploadData(penyewa_api & "?nama_penyewa=" & txtNMPENYEWA.Text, "DELETE", byteArray)

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
            Dim response As HttpResponseMessage = client.GetAsync(penyewa_api & "?nama_penyewa=" & txtNMPENYEWA.Text).Result

            ' Check if the request was successful
            If response.IsSuccessStatusCode Then
                ' Read the response content as a string
                Dim jsonString As String = response.Content.ReadAsStringAsync().Result
                Try
                    If (jsonString = "[]") Then
                        penyewa_baru = True
                        MessageBox.Show("Data Not Found")
                        Exit Sub
                    Else
                        penyewa_baru = False
                        Exit Sub
                    End If
                Catch ex As Exception

                Finally
                    If (penyewa_baru = False) Then
                        ' Deserialize the JSON into objects
                        Dim data As List(Of Penyewa) = JsonConvert.DeserializeObject(Of List(Of Penyewa))(jsonString)

                        ' Create textboxes dynamically and set their values
                        For Each mydata As Penyewa In data
                            txtNMPENYEWA.Text = mydata.nama_penyewa
                            txtALAMAT.Text = mydata.alamat
                            txtEMAIL.Text = mydata.email
                            txtTELEPON.Text = mydata.telepon
                        Next
                    End If

                End Try
            Else
                ' Request failed, handle the error
                MessageBox.Show($"Error: {response.StatusCode} - {response.ReasonPhrase}")

            End If
        End Using
    End Sub
    Private Sub GetClear()
        txtNMPENYEWA.Clear()
        txtALAMAT.Clear()
        txtEMAIL.Clear()
        txtTELEPON.Clear()
        Reloaded()
        txtNMPENYEWA.Focus()
    End Sub
    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        If (penyewa_baru = True) Then
            TambahData()
        Else
            UpdateData()
        End If
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        GetClear()
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If (penyewa_baru = False) Then
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
        Dim response As String = client.DownloadString(penyewa_api)

        ' Deserialize the JSON response into a list of objects
        Dim data As List(Of Penyewa) = JsonConvert.DeserializeObject(Of List(Of Penyewa))(response)

        ' Bind the data to the DataGridView
        dgvData.DataSource = data
    End Sub

    Private Sub txtNIM_KeyDown(sender As Object, e As KeyEventArgs) Handles txtNMPENYEWA.KeyDown
        If (e.KeyCode = Keys.Enter) Then
            GetData()
        End If
    End Sub

    Private Sub dgvData_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvData.CellContentClick

    End Sub
End Class