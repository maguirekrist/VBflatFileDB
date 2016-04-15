Imports System.IO

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
                Dim addingMems As Boolean = True
                Console.Clear()
                Do
                    Console.WriteLine("Please enter the name of new member: ")
                    tempName = Console.ReadLine()
                    Console.WriteLine("Please enter the ID of new member:")
                    tempID = Console.ReadLine()
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
                Dim tempID As Integer
                Dim tempLine As Integer
                Console.WriteLine("Enter the ID of the member you wish to delete: ")
                tempID = Console.ReadLine()
                tempLine = GetLineOfMember(tempID, stReader)
                RemoveMember(tempID, tempLine)
                Console.WriteLine("Successfully removed member " + tempID.ToString)
                GoTo Menu
            Case "ReadAll"
                ReadAll()
            Case "SearchMember"

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

    Sub RemoveMember(ByVal ID As Integer, ByVal lineOfMember As Integer)
        Dim lines() As String = System.IO.File.ReadAllLines(fileLocation)
        lines(lineOfMember - 1) = ""
        Try
            System.IO.File.WriteAllLines(fileLocation, lines)
        Catch ex As Exception

        End Try
    End Sub

    Sub ReadAll()
        Dim Lines() As String = File.ReadAllLines(fileLocation)
        For Each row In Lines
            Dim rowData() = Split(row, ", ")
            For Each data1 In rowData
                Console.WriteLine(data1)
            Next
            If row <> "" Then
                Console.WriteLine(row)
            End If
        Next
        Console.ReadLine()
    End Sub

    Function GetLineOfMember(ByVal ID As Integer, ByRef reader As StreamReader)
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
    Function SearchMember(ByVal name As String)


    End Function

    Function SearchMember(ByVal ID As Integer)


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
