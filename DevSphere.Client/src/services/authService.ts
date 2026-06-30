const API_URL = "https://localhost:7135/Auth";


//LOGIN
export async function login(data: {
    email: string;
    password: string;
}) {
    const response = await fetch(`${API_URL}/login`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        credentials: "include",
        body: JSON.stringify(data),
    });

    const result = await response.json();

    if (!response.ok || result.Success === false) {
        throw new Error(result.Message || "Something went wrong");
    }

    return result;
}


//REGISTERATION
export async function register(data: {
    username: string;
    email: string;
    password: string;
}) {
    const response = await fetch(`${API_URL}/register`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        credentials: "include",
        body: JSON.stringify(data),
    });

    const result = await response.json();

    if (!response.ok || result.Success === false) {
        throw new Error(result.Message || "Something went wrong");
    }

    return result;
}