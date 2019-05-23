"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/CharacterRegistrationHub").build();

//Disable send button until connection is established
document.getElementById("registerButton").disabled = true;

connection.on("TeamFormedReceived", function (message) {
    document.getElementById("Status").innerHTML = message;
});

connection.on("RegistrationReceived", function (tanksInQueue, healersInQueue, dpsInQueue) {
    document.getElementById("Status").innerHTML = "";
    var pt = document.createElement("p");
    pt.textContent = "Tanks in queue:" + tanksInQueue;
    document.getElementById("Status").appendChild(pt);
    var ph = document.createElement("p");
    ph.textContent = "Healers in queue:" + healersInQueue;
    document.getElementById("Status").appendChild(ph);
    var pd = document.createElement("p");
    pd.textContent = "DPS in queue:" + dpsInQueue;
    document.getElementById("Status").appendChild(pd);
});



connection.start().then(function () {
    document.getElementById("registerButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("registerButton").addEventListener("click", function (event) {
    var isValid = true;

    var cls = document.getElementById("Class").value;
    if (cls == "") {
        document.getElementById("classVerification").style.display = "block";
        isValid = false;
    }
        
    var role = document.getElementById("Role").value;
    if (role == "") {
        document.getElementById("roleVerification").style.display = "block";
        isValid = false;
    }
        
    var level = document.getElementById("Level").value;
    if (level == "") {
        document.getElementById("levelVerification").style.display = "block";
        isValid = false;
    }

    var faction = document.getElementById("Faction").value;
    if (faction == "") {
        document.getElementById("factionVerification").style.display = "block";
        isValid = false;
    }

    var name = document.getElementById("Name").value;
    if (name == "") {
        document.getElementById("nameVerification").style.display = "block";
        isValid = false;
    }

    if (isValid) {
        document.getElementById("classVerification").style.display = "none";
        document.getElementById("roleVerification").style.display = "none";
        document.getElementById("levelVerification").style.display = "none";
        document.getElementById("factionVerification").style.display = "none";
        document.getElementById("nameVerification").style.display = "none";
        document.getElementById("RegistrationInputFields").innerHTML = "";
        connection.invoke("Register", cls, role, level, faction, name).catch(function (err) {
            return console.error(err.toString());
        });
    }
    event.preventDefault();
});