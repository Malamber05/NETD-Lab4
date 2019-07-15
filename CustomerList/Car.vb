Option Strict On
Public Class Car


    Private Shared carCount As Integer                 ' static or shared private variable to hold the number of cars
    Private carIdentificationNumber As Integer = 0     ' private variable to hold the car's identification number
    Private carMake As String = String.Empty         ' private variable to hold the car's Make
    Private carModel As String = String.Empty     ' private variable to hold the car's Model
    Private carYear As String = String.Empty      ' private variable to hold the car's Year
    Private carNew As Boolean = False              ' private variable to hold if the car is new or not
    Private carPrice As String = String.Empty      ' private variable to hold the car's Price

    ''' <summary>
    ''' Constructor - Default - creates a new Car object
    ''' </summary>
    Public Sub New()

        carCount += 1      ' increment the shared/static variable counter by 1
        carIdentificationNumber = carCount ' assign the customers id

    End Sub

    ''' <summary>
    ''' Constructor - Parameterized - creates a new car object
    ''' </summary>
    ''' <param name="Make"></param>
    ''' <param name="Model"></param>
    ''' <param name="Year"></param>
    ''' <param name="IsNew"></param>
    ''' <param name="Price"></param>
    Public Sub New(Make As String, Model As String, Year As String, IsNew As Boolean, Price As String)

        ' call the other constructor 
        ' to set the customer count and
        ' to set the customer id
        Me.New()


        carMake = Make          ' set the cars make
        carModel = Model  ' set the cars Model
        carYear = Year    ' set the cars Year 
        carNew = IsNew        ' set the cars status
        carPrice = Price     ' set the cars Price

    End Sub


    ''' <summary>
    ''' Count ReadOnly Property - Gets the number of customers that have been instantiated/created
    ''' </summary>
    ''' <returns>Integer</returns>
    Public ReadOnly Property Count() As Integer
        Get
            Return carCount
        End Get
    End Property

    ''' <summary>
    ''' IdentificationNumber ReadOnly Property - Gets a specific cars identification number
    ''' </summary>
    ''' <returns>Integer</returns>
    Public ReadOnly Property IdentificationNumber() As Integer
        Get
            Return carIdentificationNumber
        End Get
    End Property

    ''' <summary>
    ''' VeryImportantPersonStatus Property - >Gets and Sets if the car is new or not
    ''' </summary>
    ''' <returns>Boolean</returns>
    Public Property IsNew() As Boolean
        Get
            Return carNew
        End Get
        Set(ByVal value As Boolean)
            carNew = value
        End Set
    End Property
    ''' <summary>
    ''' Title property - Gets and Sets the Make of the car
    ''' </summary>
    ''' <returns>String</returns>
    Public Property Make() As String
        Get
            Return carMake
        End Get
        Set(ByVal value As String)
            carMake = value
        End Set
    End Property

    ''' <summary>
    ''' Title property - Gets and Sets the Model of the car
    ''' </summary>
    ''' <returns>String</returns>
    Public Property Model() As String
        Get
            Return carModel
        End Get
        Set(ByVal value As String)
            carModel = value
        End Set
    End Property

    ''' <summary>
    ''' FirstName property - Gets and Sets the Year of the car 
    ''' </summary>
    ''' <returns>String</returns>
    Public Property Year() As String
        Get
            Return carYear
        End Get
        Set(ByVal value As String)
            carYear = value
        End Set
    End Property

    ''' <summary>
    ''' LastName property - Gets and Sets the Price of the car 
    ''' </summary>
    ''' <returns>String</returns>
    Public Property Price() As String
        Get
            Return carPrice
        End Get
        Set(ByVal value As String)
            carPrice = value
        End Set
    End Property

    ''' <summary>
    ''' GetSalutation is a function that a salutation based on the private properties within the class scope
    ''' </summary>
    ''' <returns>String</returns>
    Public Function GetSalutation() As String

        Return "The chosen car is a " & IIf(carNew = True, "New ", "Used ").ToString() & carYear & " " & carMake & " " & carModel & ", being sold for " & carPrice

    End Function


End Class
