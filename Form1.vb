'Elizabeth Anderson
'Sequential Access and Files Programming Assignment
'This program aloows member to be added to 
'11/28/2022
Imports System.Diagnostics.Eventing.Reader
Imports System.IO

Public Class frmAssignment5
    Dim temp_directory = "C:\temp\"
    Dim strMembersfile As String
    Dim Count As Integer
    Private Sub frmAssignment5_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' loads name on selected file
        strMembersfile = temp_directory & "Youth_Members-2.txt"
        lstMembers.DataSource = IO.File.ReadAllLines(strMembersfile)
        lstMembers.SelectedItem = Nothing
        Count = lstMembers.Items.Count()
        txtCount.Text = Count
    End Sub
    Sub refreshlist()
        lstMembers.DataSource = IO.File.ReadAllLines(strMembersfile)
        lstMembers.SelectedItem = Nothing
        Count = lstMembers.Items.Count()
        txtCount.Text = Count
    End Sub
    Private Sub AscendToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AscendToolStripMenuItem.Click
        'LINQ which sorts names ascending
        Dim Ascendquery = From name In IO.File.ReadAllLines(strMembersfile)
                          Order By name Ascending
                          Select name
        lstMembers.DataSource = Ascendquery.ToList
    End Sub
    Private Sub DescendToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DescendToolStripMenuItem.Click
        'sorts name descending 
        Dim Descendquery = From name In IO.File.ReadAllLines(strMembersfile)
                           Order By name Descending
                           Select name
        lstMembers.DataSource = Descendquery.ToList
    End Sub
    Private Sub AddMemberToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AddMemberToolStripMenuItem.Click
        ' function gets a member name and will not come back until it gets a good one
        Dim Newmember As String = input_member()
        If Newmember = String.Empty Then
            Return
        End If
        Dim SW As IO.StreamWriter
        SW = IO.File.AppendText(strMembersfile)
        ' Seperating these lines allows the code to work right 
        ' when in one line name goes on the same line as the pervious name instead of creating a new line 
        ' IDK why but seperating them fixed the issue 
        SW.WriteLine(Newmember)
        SW.Close()
        refreshlist()
    End Sub

    Private Function input_member() As String
        Dim new_member As String = ""
        Dim valid_member As Boolean = False

        ' This while block disables the cancel button / What to do about this? 
        While Not valid_member
            new_member = InputBox("Please enter New member's Full name")
            If new_member = " " Then
                MessageBox.Show("Please enter a name")
            ElseIf new_member = String.Empty Then
                ' Turns out that the Cancel button returns String.Empty
                ' so just make that valid here, and check for empty string from the calling event
                valid_member = True
                'MessageBox.Show("Please enter a REAL name")
            Else
                valid_member = True
            End If
        End While
        Return new_member
    End Function

    Private Sub DeleteMemberToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteMemberToolStripMenuItem.Click
        'Error Check not needed as button is not enabled unless a name is selected 
        Dim deleteMember As String = lstMembers.SelectedItem
        Dim deleteQuery = From reading In IO.File.ReadAllLines(strMembersfile)
                          Where reading <> deleteMember
                          Select reading
        IO.File.WriteAllLines(strMembersfile, deleteQuery)
        refreshlist()
    End Sub
    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        'Quits Program
        Close()
    End Sub

    Private Sub lstMembers_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstMembers.SelectedIndexChanged
        ' Error check for delete, only enables delete button if the user has selected a name from list 
        If lstMembers.SelectedItems.Count = 0 Then
            DeleteMemberToolStripMenuItem.Enabled = False
        Else
            DeleteMemberToolStripMenuItem.Enabled = True
        End If
    End Sub

    Private Sub CreateFileToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CreateFileToolStripMenuItem.Click
        'file name
        Dim strnewfile As String

        strnewfile = InputBox("Please enter file name ")
        If strnewfile = " " Then
            MessageBox.Show("Enter the new file's name")
        ElseIf strnewfile = String.Empty Then
            ' when user clicks Cancel, we get String.Empty
            '            MessageBox.Show("Enter the new file's name")
            Exit Sub 'yeah this is a bit crap. It should work though.
            ' Better would be to reuse the input_member function
            ' you would rename it to input_text and pass in the right prompts
            ' I am sure there is a proper way to do all this, with bits of .NET, but you certainly haven't been shown what that might be.
            ' This is just me making it up as I go along, with my own code that is vaguely usefulish.
        End If
        'Same issue as last IF statement, how to return to Inputbox 
        strMembersfile = temp_directory & strnewfile & ".txt"
        ' if statement to check if it exisit and if if doesn't create it 
        If Not IO.File.Exists(strMembersfile) Then
            Dim SW As IO.StreamWriter = IO.File.CreateText(strMembersfile)
            SW.Close()
        End If
        MsgBox("Created file " & strMembersfile) ' because they won't necessarily know where it is, or what is going on
        refreshlist()
    End Sub
End Class
