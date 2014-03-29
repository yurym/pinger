Imports System.Net.NetworkInformation
'Imports Un4seen.BassMOD

Public Class ping_frm

    Dim host As String
    Dim count As Int32 = 0

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Timer1.Interval = 1000
        cat_load()
        'BassMOD.BASSMOD_Init(-1, 44100, 0)
        'BassMOD.BASS_MusicLoad("d:\Downloads\Chiptunes\_) - WinRAR and RAR unblacklister.xm", 0, 0, 0)
        'BassMOD.BASSMOD_MusicPlay()
    End Sub

    Private Sub cat_load()
        ListBox1.Items.Insert(0, "")
        ListBox1.Items.Insert(1, "                      *     ,MMM8&&&.            *")
        ListBox1.Items.Insert(2, "                           MMMM88&&&&&    .")
        ListBox1.Items.Insert(3, "                          MMMM88&&&&&&&")
        ListBox1.Items.Insert(4, "              *           MMM88&&&&&&&&")
        ListBox1.Items.Insert(5, "                          MMM88&&&&&&&&")
        ListBox1.Items.Insert(6, "                          'MMM88&&&&&&'")
        ListBox1.Items.Insert(7, "                            'MMM8&&&'      *")
        ListBox1.Items.Insert(8, "                   |\___/|     /\___/\")
        ListBox1.Items.Insert(9, "                   )     (     )    ~( .              '")
        ListBox1.Items.Insert(10, "                  =\     /=   =\~    /=")
        ListBox1.Items.Insert(11, "                    )===(       ) ~ (")
        ListBox1.Items.Insert(12, "                   /     \     /     \")
        ListBox1.Items.Insert(13, "                   |     |     ) ~   (")
        ListBox1.Items.Insert(14, "                  /       \   /     ~ \")
        ListBox1.Items.Insert(15, "                  \       /   \~     ~/")
        ListBox1.Items.Insert(16, "           jgs_/\_/\__  _/_/\_/\__~__/_/\_/\_/\_/\_/\_")
        ListBox1.Items.Insert(17, "           |  |  |  |( (  |  |  | ))  |  |  |  |  |  |")
        ListBox1.Items.Insert(18, "           |  |  |  | ) ) |  |  |//|  |  |  |  |  |  |")
        ListBox1.Items.Insert(19, "           |  |  |  |(_(  |  |  (( |  |  |  |  |  |  |")
        ListBox1.Items.Insert(20, "           |  |  |  |  |  |  |  |\)|  |  |  |  |  |  |")
        ListBox1.Items.Insert(21, "           |  |  |  |  |  |  |  |  |  |  |  |  |  |  |")
    End Sub

    Private Sub stop_ping()
        Button1.Text = "Пинг!"
        Timer1.Stop()
    End Sub

    Private Sub start_ping()
        Button1.Text = "Стоп"
        host = TextBox1.Text
        Timer1.Start()
    End Sub

    Private Sub clear_log()
        While ListBox1.Items.Count > 24
            ListBox1.Items.RemoveAt(ListBox1.Items.Count - 1)
        End While
    End Sub

    Private Sub clear_area()
        ListBox1.Items.Clear()
        count = 0
        Label1.Text = 0
        cat_load()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Button1.Text = "Пинг!" Then
            start_ping()
        Else
            stop_ping()
        End If
    End Sub

    Private Sub MaskedTextBox1_KeyDown(sender As Object, e As KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            If Button1.Text = "Пинг!" Then
                start_ping()
            Else
                host = TextBox1.Text
            End If
        End If
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If BackgroundWorker1.IsBusy = False Then
            BackgroundWorker1.RunWorkerAsync(host)
        End If
    End Sub

    Private Sub BackgroundWorker1_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Dim pingreq As Ping = New Ping
        Try
            Dim rep As PingReply = pingreq.Send(e.Argument)
            e.Result = " - Ответ от " + rep.Address.ToString + ": время=" + rep.RoundtripTime.ToString + "мс TTL=" + rep.Options.Ttl.ToString
        Catch ex As PingException
            e.Result = " - Проверьте имя узла и повторите попытку"
        Catch ex As NullReferenceException
            e.Result = " - Заданный узел недоступен"
        Catch ex As ArgumentNullException
            e.Result = " - Значение не может быть неопределенным"
        End Try
    End Sub

    Private Sub BackgroundWorker1_RunWorkerCompleted(sender As Object, e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted
        ListBox1.Items.Insert(0, DateTime.Now + e.Result)
        clear_log()
        If Not e.Result.ToString.Contains("Ответ") Then
            count = count + 1
            Label1.Text = count
        End If
    End Sub

    Private Sub Form1_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles MyBase.MouseDoubleClick
        clear_area()
    End Sub

    Private Sub ListBox1_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles ListBox1.MouseDoubleClick
        clear_area()
    End Sub
End Class
