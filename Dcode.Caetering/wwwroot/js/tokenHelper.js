export function getTokenFromLocalStorage() {
    return localStorage.getItem("authToken") || "";
}