function GetDate() {
    let current = new Date();
    document.getElementById("time").innerHTML = current;

    FutureDate(current);

    return (current);
}

function FutureDate(today) {
    let future = today;

    future.setMonth(future.getMonth() + 3);

    document.getElementById("threeMonths").innerHTML = future;

    return (future);
}