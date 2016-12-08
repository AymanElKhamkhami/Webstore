<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="UIP_Webstore._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <h1><%: Title %>.</h1>

    <div class="jumbotron">
        <h1>PC Games</h1>
        <p class="lead">Get the best PC games online from Microsoft. Browse our wide selection of favorites, with everything from movie-based games to fantasy games, racing games, military games, sim games, children's games, and more. Shop Microsoft to find low prices on the hottest new releases.</p>
        <p><a href="http://www.asp.net" class="btn btn-primary btn-lg">Read more &raquo;</a></p>
    </div>

    <div class="row">
        <div class="col-md-4">
            <h2>Games Overviews</h2>
            <p>
                When you click on a game to learn more, you'll find detailed information about its premise, gaming requirements, and more. Get an overview of the game's features and decide whether it's something you want to play. Read reviews from other players to discover the possible pros and cons of each game.
            </p>
            <p>
                <a class="btn btn-default" href="http://go.microsoft.com/fwlink/?LinkId=301948">Learn more &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>ESRB Rating</h2>
            <p>
                Not sure whether a game is suitable for your children or your tastes? No problem. The page for each of our PC games online provides information on the game's ESRB rating – from E for Everyone to T for Teen, M for Mature, and A for Adults Only. Find out why the game received the rating it did thanks to rating information that includes mention of language, themes, violence, blood and gore, drug references, and more.
            </p>
            <p>
                <a class="btn btn-default" href="http://go.microsoft.com/fwlink/?LinkId=301949">Learn more &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>The Best Games Selection</h2>
            <p>
                At Games webstore, experience convenience, affordable prices, and a wide selection of the best PC games. Browse our games today, and see what Microsoft has in store. 
            </p>
            <p>
                <a class="btn btn-default" href="http://go.microsoft.com/fwlink/?LinkId=301950">Learn more &raquo;</a>
            </p>
        </div>
    </div>

</asp:Content>
