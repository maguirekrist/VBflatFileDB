Imports System.IO
Imports System.Text.RegularExpressions


Module Module1

    Dim fileLocation As String = "C:\Users\s25737610\Desktop\DataFiles\Lab6.dat"

    Sub Main()
        ' Database file example: Name(String), TelePhoneNumber(String), Membership start date (DateTime), ID(PrimaryKey)
        Dim stWriter As StreamWriter
        Dim stReader As StreamReader
        Dim choice As String
menu:
        Console.WriteLine("Welcome to the membership databank menu!")
failSafe1:
        Console.WriteLine("|Add Member|Remove Member|ReadAll|SearchMember|Exit")
        choice = Console.ReadLine()
        Select Case choice
            Case "Add Member"
                Dim tempName As String
                Dim tempID As String
                Dim tempTele As String
                Dim tempDate As String
                Dim addingMems As Boolean = True
                Dim regexID As String = "^[A-Z]{1}[a-z]{2}[0-9]{3}$"
                Dim regexDate As String = "^[0-9]{2}/[0-9]{2}/[0-9]{2}$"
                Dim regexTele As String = "^[0-9]{3}-[0-9]{3}-[0-9]{5}$"
                Console.Clear()
                Do
                    Console.WriteLine("Please enter the name of new member: ")
                    tempName = Console.ReadLine()
failSafe2:
                    Console.WriteLine("Please enter the ID of new member:")
                    tempID = Console.ReadLine()
                    If Regex.IsMatch(tempID, regexID) Then

                    Else
                        Console.WriteLine("Invalid format for ID")
                        GoTo failSafe2
                    End If
failSafe3:
                    Console.WriteLine("Please enter the Telephone of the new memeber:")
                    tempTele = Console.ReadLine()
                    If Regex.IsMatch(tempTele, regexTele) Then

                    Else
                        Console.WriteLine("Invalid format for Telephone")
                        GoTo failSafe3
                    End If
                    AddMember(tempName, tempID, stWriter)
                    Console.WriteLine("Continue adding another member? (Y,N): ")
                    If Console.ReadLine = "Y" Then
                        addingMems = True
                    Else
                        addingMems = False
                        GoTo menu
                    End If
                Loop Until addingMems = False
            Case "Remove Member"
removemember:
                Dim tempID As String
                Dim tempLine As Integer
                Console.WriteLine("Enter the ID of the member you wish to delete: ")
                tempID = Console.ReadLine()
                tempLine = GetLineOfMember(tempID, stReader)
                RemoveMember(tempID, tempLine)
                Console.WriteLine("Successfully removed member " + tempID.ToString)
                Console.WriteLine("remove another member? (Y/N)")
                If Console.ReadLine = "Y" Then
                    GoTo removemember
                Else
                    GoTo menu
                End If
            Case "ReadAll"
                ReadAll()
                GoTo Menu
            Case "SearchMember"
searchmember:
                Console.WriteLine("Please enter the ID or Name of user: ")
                Dim searchParam = Console.ReadLine()
                Console.WriteLine(SearchMember(searchParam))
                Console.WriteLine("Search for another member? (Y,N)")
                If Console.ReadLine = "Y" Then
                    GoTo searchmember
                Else
                    GoTo menu
                End If
            Case "Exit"
                Console.WriteLine("GoodBye")
                ExitProgram(stWriter)
            Case Else
                Console.WriteLine("Not a valid choice!")
                GoTo failSafe1
        End Select


    End Sub

    Sub ExitProgram(ByRef writer As StreamWriter)
        Try
            writer.Close()
        Catch ex As Exception
            Console.WriteLine(ex)
        End Try
    End Sub

    Sub AddMember(ByVal name As String, ByVal ID As String, ByRef writer As StreamWriter)
        OpenFileWriter(writer)
        writer.WriteLine(ID + ", " + name)
        CloseFileWriter(writer)
    End Sub

    Sub RemoveMember(ByVal ID As String, ByVal lineOfMember As Integer)
        Dim lines() As String = System.IO.File.ReadAllLines(fileLocation)
        lines(lineOfMember - 1) = ""
        Try
            System.IO.File.WriteAllLines(fileLocation, lines)
        Catch ex As Exception

        End Try
    End Sub

    Sub ReadAll()
        Dim Lines() As String = File.ReadAllLines(fileLocation)
        Console.WriteLine("---------")
        Console.WriteLine("ID     name")
        For Each row In Lines
            Dim rowData() = Split(row, ", ")
            If row <> "" Then
                Console.WriteLine(rowData(0) + "     " + rowData(1))
            End If
        Next
        Console.ReadLine()
    End Sub

    Function GetLineOfMember(ByVal ID As String, ByRef reader As StreamReader)
        OpenFileReader(reader)
        Dim lineNum As Integer
        Dim data As String()
        While Not reader.EndOfStream
            lineNum = lineNum + 1
            Dim line As String = reader.ReadLine()
            data = line.Split(", ")
            If data(0) = ID.ToString Then
                CloseFileReader(reader)
                Return lineNum
            End If
        End While
        Return 0
        CloseFileReader(reader)
    End Function


    'Overloading search function, it may return more than 1 member.
    Function SearchMember(ByVal data As String)
        Dim regexReq As String = "^[A-Z]{1}[a-z]{2}[0-9]{3}$"
        Dim lines() = File.ReadAllLines(fileLocation)
        For Each line In lines
            If line.Contains(data) Then
                Dim info() As String = Split(line, ", ")
                Console.WriteLine("User found: ")
                Console.WriteLine("ID      NAME        ")
                Return line
            Else
                Continue For
            End If
        Next
        Return "ERROR UNKNOWN!"
    End Function

    'Utility Function to manage the opening and the closing of our IO system

    Sub OpenFileWriter(ByRef writer As StreamWriter)
        writer = New StreamWriter(fileLocation, True)
    End Sub

    Sub CloseFileWriter(ByRef writer As StreamWriter)
        writer.Close()
    End Sub

    Sub OpenFileReader(ByRef reader As StreamReader)
        reader = New StreamReader(fileLocation)
    End Sub

    Sub CloseFileReader(ByRef reader As StreamReader)
        reader.Close()
    End Sub

End Module
