
Option Strict On
Imports System.Text.RegularExpressions

''' <summary>
''' Author Name:    Alex Blizzard
''' Project Name:   CarList
''' Date:           15-Mar-2019
''' Description     Application that will let the user enter Car informaton and put it into a table, which can later be edited.
'''                 The car can be listed as New or Used, and also the Price can be stated. If wanted, the already existing cars in the list
'''                 can be edited by selecting it form the list view and modified to the users pleasure and then it is saved.
'''                 The price is validated, making sure that the number is actually a number, or is not left blank. Using Regular Expressions
'''                 After the number is validated, the price is put into the format; if not already put in by the user. (2 decimal places,
'''                 and a number sign at the beginning.
''' </summary>

Public Class frmCarList

    Private carList As New SortedList                                 ' collection of all the customerList in the list
    Private currentCarIdentificationNumber As String = String.Empty ' current selected customer identification number
    Private editMode As Boolean = False                                 '

    ''' <summary>
    ''' btnEnter_Click - Will validate that the data entered into the controls is appropriate.
    '''                - Once the data is validated a customer object will be create using the  
    '''                - parameterized constructor. It will also insert the new Car object
    '''                - into the CarList collection. It will also check to see if the data in
    '''                - the controls has been selected from the listview by the user. In that case
    '''                - it will need to update the data in the specific Car object and the 
    '''                - listview as well.
    ''' </summary>
    ''' <param name="sender">Object</param>
    ''' <param name="e">EventArgs</param>
    Private Sub btnEnter_Click(sender As Object, e As EventArgs) Handles btnAccept.Click

        Dim thisCar As Car            ' declare a Car class
        Dim carItem As ListViewItem    ' declare a ListViewItem class

        ' validate the data in the form
        If IsValidInput() = True Then

            editMode = True

            ' if the current customer identification number has a no value
            ' then this is not an existing item from the listview
            If currentCarIdentificationNumber.Trim.Length = 0 Then

                ' create a new customer object using the parameterized constructor
                thisCar = New Car(cmbMake.Text, tbModel.Text, cmbYear.Text, chkNew.Checked, tbPrice.Text)

                ' add the customer to the customerList collection
                ' using the identoification number as the key
                ' which will make the customer object easier to
                ' find in the customerList collection later
                carList.Add(thisCar.IdentificationNumber.ToString(), thisCar)

            Else
                ' if the current customer identification number has a value
                ' then the user has selected something from the list view
                ' so the data in the customer object in the customerList collection
                ' must be updated

                ' so get the customer from the custmers collection
                ' using the selected key
                thisCar = CType(carList.Item(currentCarIdentificationNumber), Car)

                ' update the data in the specific object
                ' from the controls
                thisCar.Make = cmbMake.Text
                thisCar.Model = tbModel.Text
                thisCar.Year = cmbYear.Text
                thisCar.IsNew = chkNew.Checked
                thisCar.Price = tbPrice.Text
            End If

            ' clear the items from the listview control
            lvwCars.Items.Clear()

            ' loop through the customerList collection
            ' and populate the list view
            For Each carEntry As DictionaryEntry In carList

                ' instantiate a new ListViewItem
                carItem = New ListViewItem()

                ' get the customer from the list
                thisCar = CType(carEntry.Value, Car)

                ' assign the values to the ckecked control
                ' and the subitems
                carItem.Checked = thisCar.IsNew
                carItem.SubItems.Add(thisCar.IdentificationNumber.ToString())
                carItem.SubItems.Add(thisCar.Make)
                carItem.SubItems.Add(thisCar.Model)
                carItem.SubItems.Add(thisCar.Year)
                carItem.SubItems.Add(thisCar.Price)

                ' add the new instantiated and populated ListViewItem
                ' to the listview control
                lvwCars.Items.Add(carItem)

            Next carEntry

            ' Alternate looping strategy
            'For index As Integer = 0 To customerList.Count - 1

            '    ' instantiate a new ListViewItem
            '    customerItem = New ListViewItem()

            '    ' get the customer from the list
            '    customer = CType(customerList(customerList.GetKey(index)), Car)

            '    ' assign the values to the ckecked control
            '    ' and the subitems
            '    customerItem.Checked = customer.VeryImportantPersonStatus
            '    customerItem.SubItems.Add(customer.IdentificationNumber.ToString())
            '    customerItem.SubItems.Add(customer.Title)
            '    customerItem.SubItems.Add(customer.FirstName)
            '    customerItem.SubItems.Add(customer.LastName)

            '    ' add the new instantiated and populated ListViewItem
            '    ' to the listview control
            '    lvwCustomers.Items.Add(customerItem)

            'Next index

            lbResult.Text = "It worked!"

            ' clear the controls
            Reset()

            ' set the edit flag to false
            editMode = False

        End If

    End Sub

    ''' <summary>
    ''' Reset - set the controls back to their default state.
    ''' </summary>
    Private Sub Reset()


        tbModel.Text = String.Empty
        tbPrice.Text = String.Empty
        chkNew.Checked = False
        cmbMake.SelectedIndex = -1
        cmbYear.SelectedIndex = -1
        lbResult.Text = String.Empty
        cmbMake.Focus()

        currentCarIdentificationNumber = String.Empty

    End Sub

    ''' <summary>
    ''' IsValidInput - validates the data in each control to ensure that the user has entered apprpriate values
    ''' </summary>
    ''' <returns>Boolean</returns>
    Private Function IsValidInput() As Boolean

        Dim returnValue As Boolean = True
        Dim outputMessage As String = String.Empty

        ' check if the title has been selected
        If cmbMake.SelectedIndex = -1 Then

            ' If not set the error message
            outputMessage += "Please select a Make" & vbCrLf

            ' And, set the return value to false
            returnValue = False

        End If

        ' check if the first name has been entered
        If tbModel.Text.Trim.Length = 0 Then

            ' If not set the error message
            outputMessage += "Please enter a Model name." & vbCrLf

            ' And, set the return value to false
            returnValue = False

        End If

        If cmbYear.SelectedIndex = -1 Then

            ' If not set the error message
            outputMessage += "Please select a Year" & vbCrLf

            ' And, set the return value to false
            returnValue = False

        End If

        ' check if the price is in the correct format
        If Not Regex.Match(tbPrice.Text.ToString(), "^\$?[0-9]+(\.[0-9]{2})?$").Success Then
            ' If not set the error message
            outputMessage += "Please enter a Valid Price. Ex. (1000.99)" & vbCrLf

            ' And, set the return value to false
            returnValue = False
        Else
            If Regex.Match(tbPrice.Text.ToString(), "^\$?[0-9]+$").Success Then
                tbPrice.Text = tbPrice.Text.ToString() & ".00"
            End If
            If Not Regex.Match(tbPrice.Text.ToString(), "\$.").Success Then
                tbPrice.Text = "$" & tbPrice.Text.ToString()
            End If
        End If
        ' check to see if any value
        ' did not validate
        If returnValue = False Then

            ' show the message(s) to the user
            lbResult.Text = "ERROR(S)" & vbCrLf & outputMessage

        End If

        ' return the boolean value
        ' true if it passed validation
        ' false if it did not pass validation
        Return returnValue

    End Function

    ''' <summary>
    ''' Event is declared as private because it is only accessible within the form
    ''' The code in the btnReset_Click EventHandler will clear the form and set
    ''' focus back to the input text box. 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnCancel.Click

        ' call the rest sub routine
        Reset()

    End Sub
    ''' <summary>
    ''' Event is declared as private because it is only accessible within the form
    ''' The code in the btnExit_Click EventHandler will close the application
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click

        ' This will close the form
        Me.Close()

    End Sub

    ''' <summary>
    ''' lvwCustomers_ItemCheck - used to prevent the user from checking the check box in the list view
    '''                        - if it is not in edit mode
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub lvwCustomers_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles lvwCars.ItemCheck

        ' if it is not in edit mode
        If editMode = False Then

            ' the new value to the current value
            ' so it cannot be set in the listview by the user
            e.NewValue = e.CurrentValue

        End If

    End Sub

    ''' <summary>
    ''' lvwCustomers_SelectedIndexChanged - when the user selected a row in the list it will populate the fields for editing
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub lvwCustomers_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lvwCars.SelectedIndexChanged

        ' constant that represents the index of the subitem in the list that
        ' holds the customer identification number 
        Const identificationSubItemIndex As Integer = 1

        ' Get the customer identification number 
        currentCarIdentificationNumber = lvwCars.Items(lvwCars.FocusedItem.Index).SubItems(identificationSubItemIndex).Text

        ' Use the customer identification number to get the customer from the collection object
        Dim thisCar As Car = CType(carList.Item(currentCarIdentificationNumber), Car)

        ' set the controls on the form
        tbModel.Text = thisCar.Model             ' get the ccar model and set the text box
        tbPrice.Text = thisCar.Price                 ' get the car Price and set the text box
        cmbMake.Text = thisCar.Make                     ' get the car Make and set the combo box
        chkNew.Checked = thisCar.IsNew             ' get's whether the new box is checked
        cmbYear.Text = thisCar.Year             ' get the Year from a combo box

        lbResult.Text = thisCar.GetSalutation()


    End Sub

    Private Sub frmCarList_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim D As New Date
        D = Now
        For x = D.Year To 1900 Step -1
            cmbYear.Items.Add(x.ToString())
        Next

        Reset()

    End Sub



    'Private Sub lvwCustomers_Click(sender As Object, e As EventArgs) Handles lvwCustomers.Click
    '    lbResult.Text = "aaa"
    'End Sub
End Class

