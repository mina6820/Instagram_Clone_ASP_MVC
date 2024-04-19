var connection = new signalR.HubConnectionBuilder()
    .withUrl("/NotificationHub")
    .build();

//window.connection = connection; // Assigning to window for global access
