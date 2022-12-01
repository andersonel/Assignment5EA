'Elizabeth Anderson
'Sequential Access and Files Programming Assignment
'This program aloows member to be added to 
'11/28/2022

Imports System.Runtime.Remoting.Messaging

Public Class frmAssignment5
    Dim strMembersfile As String
    Dim Count As Integer
    Private Sub frmAssignment5_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' COMMENT
        strMembersfile = "Youth_Members-2.txt"
        lstMembers.DataSource = IO.File.ReadAllLines(strMembersfile)
        lstMembers.SelectedItem = Nothing
        Count = lstMembers.Items.Count()
        txtCount.Text = Count
    End Sub
    Private Sub AscendToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AscendToolStripMenuItem.Click
        ' This looks fun. This is LINQ, I thinq. Did this get generated or did you write it yourself?
        Dim Ascendquery = From name In IO.File.ReadAllLines(strMembersfile)
                          Order By name Ascending
                          Select name
        lstMembers.DataSource = Ascendquery.ToList
    End Sub
    Private Sub DescendToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DescendToolStripMenuItem.Click
        Dim Descendquery = From name In IO.File.ReadAllLines(strMembersfile)
                           Order By name Descending
                           Select name
        lstMembers.DataSource = Descendquery.ToList
    End Sub
    Private Sub AddMemberToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AddMemberToolStripMenuItem.Click
        ' I am commenting out your original code, rather than replacing it
        'Dim Newmember As String = InputBox("Please enter New member's Full name")
        ' If Newmember = " " Then
        ' MessageBox.Show("Please enter a REAL name")
        ' 'HOW
        ' ElseIf Newmember = "" Then
        ' MessageBox.Show("Please enter a name")
        ' Return 'input box leaxes completley, needs to return to input box
        ' 'option to have message say try again ? 
        ' End If

        ' Use a function to go get a member name. It will not come back until it gets a good one
        Dim Newmember As String = input_member()

        '        Dim SW As IO.StreamWriter
        '        SW = IO.File.AppendText(strMembersfile)
        ' probably should do it in one line instead
        ' I think Using/End Using is a good idea
        ' your instructor will like it if they do Python because that's how you're supposed to do it there
        'https://learn.microsoft.com/en-us/dotnet/api/system.io.file.appendtext?view=net-6.0#examples
        Dim SW As IO.StreamWriter = IO.File.AppendText(strMembersfile)

        SW.WriteLine(Newmember)
        SW.Close()
        lstMembers.DataSource = IO.File.ReadAllLines(strMembersfile)
        lstMembers.SelectedItem = Nothing
        Count = lstMembers.Items.Count()
        txtCount.Text = Count
    End Sub

    Private Function input_member() As String
        ' completely untested, but probably super close to working.
        Dim new_member As String = ""
        Dim valid_member As Boolean = False

        ' This while block will run over and over, while that condition (Not valid_member) is true.
        While Not valid_member
            new_member = InputBox("Please enter New member's Full name")
            If new_member = " " Then
                MessageBox.Show("Please enter a name")
            ElseIf new_member = String.Empty Then
                MessageBox.Show("Please enter a REAL name")
            ElseIf new_member = "(you can add other checks if you want, with more ElseIfs)" Then
                MessageBox.Show("Super Specific Error Message")
            Else
                ' we have checked all ways of the input being bad,
                ' all checks have passed so we should exit the while loop.
                valid_member = True
            End If
        End While

        Return new_member
    End Function

    Private Sub DeleteMemberToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteMemberToolStripMenuItem.Click
        'Error Check something is selected 
        If lstMembers.SelectedIndex = -1 Then
            ' Maybe you could enable/disable the delete item itself, maybe in lstMembers_SelectedIndexChanged?
            MessageBox.Show("No member selected to delete")
            Return
        End If
        Dim deleteMember As String = lstMembers.SelectedItem
        Dim deleteQuery = From reading In IO.File.ReadAllLines(strMembersfile)
                          Where reading <> deleteMember
                          Select reading
        IO.File.WriteAllLines(strMembersfile, deleteQuery)
        lstMembers.DataSource = IO.File.ReadAllLines(strMembersfile)
        lstMembers.SelectedItem = Nothing
        Count = lstMembers.Items.Count()
        txtCount.Text = Count
    End Sub
    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        'Quits Program
        Close()
    End Sub

    Private Sub lstMembers_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstMembers.SelectedIndexChanged
        '        If lstMembers.SelectedItems.Count == 0 Then
        '        DeleteMemberToolStripMenuItem.Enabled = False
        '        Else
        '        DeleteMemberToolStripMenuItem.Enabled = True
        '        End If

        ' a quicker way to do the same thing: (brackets not really needed, but it might make it clearer for you
        DeleteMemberToolStripMenuItem.Enabled = (lstMembers.SelectedItems.Count <> 0)
    End Sub
End Class
