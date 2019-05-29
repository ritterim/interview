<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="InterviewDemo._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>RIMdev Technical Interview</h1>
        <p class="lead">
            The purpose of this exercise is to understand an applicants ability to problem solve, think critically, and write software with existing RIMdev team members. While there is no expected answer, we do expect the production of a working sample. That means the work must compile, transpile, or interpret and above all else run with no immediate exceptions.
            <br />

        </p>
        <p>While accomplishing this exercise is permitted before interviewing at Ritter Insurance Marketing, it may also be completed in collaboration with the interviewers.</p>
    </div>

    <div class="row">
        <div class="col-md-12">
            <h2>Objectives</h2>
            <p>
                Import data.json into memory
                
 
            </p>
            <p>
                Submit an application that answers the following questions about the data set:
            </p>
        </div>


    </div>
    <div class="row">
        <div class="col-md-12">
            <h2>Answers</h2>
            <p id="q1" runat="server">What is the count of individuals over the age of 50? </p>
            <p id="q2" runat="server">Who is last individual that registered who is still active? </p>
            <p id="q3" runat="server">What are the counts of each favorite fruit? </p>
            <p id="q4" runat="server">What is the most common eye color? </p>
            <p id="q5" runat="server">What is the total balance of all individuals combined? </p>
            <p id="q6" runat="server">What is the full name of the individual with the id of 5aabbca3e58dc67745d720b1 in the format of lastname, firstname? </p>

        </div>

    </div>
     <div class="row">
        <div class="col-md-12">
            <h2>Documentation</h2>
            <p>
               I created a json file from the data set and created an embedded resource within the project.  
               I then created 2 classes to convert the JSON string to a strongly typed class object and finally used multiple linq queries to filter the data set to answer the questions.
 
            </p>
           
        </div>


    </div>
</asp:Content>
