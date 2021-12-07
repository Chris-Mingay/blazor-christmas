import {browser} from '$app/env';

export function browserGet(key){
	if(browser){
		const item = localStorage.getItem(key);
		return item;
		/*if(item){
			return item;
		}
		return null;*/
	}
}

export function browserSet(key, value){
	if(browser){
		if(value == null){
			localStorage.removeItem(key);
		}else{
			localStorage.setItem(key, value);
		}
	}
}

export async function post(fetch, url, body){
	let customError = false;
	try {
		let headers = {}
		if(!(body instanceof FormData)){
			headers['Content-Type'] = 'application/json';
			body = JSON.stringify(body);
		}
		const token = browserGet('jwt');
		if(token){
			headers['Authorization'] = `Bearer ${token}`;
		}

		const res = await fetch(url, {
			method: 'POST',
			body,
			headers
		});
		if(!res.ok){
			try {
				const data = await res.json();
				const error = data.message[0].messages[0];
				customError = true;
				throw {id: error.id, message: error.message}
			} catch (err) {
				console.log(err);
				throw err;
			}
		}

		try {
			const json = await res.json();
			return json;
		} catch (err){
			console.log(err);
			throw {id:'',message: 'An unknown error has occurred'};
		}

	} catch (err){
		console.log(err);
		throw customError ? err : {id: '', message: 'An unknown error has occured'}
	}
}

export async function put(fetch, url, body){
	let customError = false;
	try {
		let headers = {}
		if(!(body instanceof FormData)){
			headers['Content-Type'] = 'application/json';
			body = JSON.stringify(body);
		}
		const token = browserGet('jwt');
		if(token){
			headers['Authorization'] = `Bearer ${token}`;
		}

		const res = await fetch(url, {
			method: 'PUT',
			body,
			headers
		});
		if(!res.ok){
			try {
				const data = await res.json();
				const error = data.message[0].messages[0];
				customError = true;
				throw {id: error.id, message: error.message}
			} catch (err) {
				console.log(err);
				throw err;
			}
		}

		try {
			const json = await res.json();
			return json;
		} catch (err){
			console.log(err);
			throw {id:'',message: 'An unknown error has occurred'};
		}

	} catch (err){
		console.log(err);
		throw customError ? err : {id: '', message: 'An unknown error has occured'}
	}
}


export async function get(fetch, url, params = {}){
	let customError = false;
	try {
		let headers = {}
		const token = browserGet('jwt');
		if(token){
			headers['Authorization'] = `Bearer ${token}`;
		}

		console.log(`${url}?${new URLSearchParams(params || {})}`);
		const res = await fetch(`${url}?${new URLSearchParams(params || {})}`, {
			method: 'GET',
			headers
		});
		if(!res.ok){
			try {
				const data = await res.json();
				console.log(data);
				const error = data.message[0].messages[0];
				customError = true;
				throw {id: error.id, message: error.message}
			} catch (err) {
				console.log(err);
				throw err;
			}
		}

		try {
			const json = await res.json();
			return json;
		} catch (err){
			console.log(err);
			throw {id:'',message: 'An unknown error has occurred'};
		}

	} catch (err){
		console.log(err);
		throw customError ? err : {id: '', message: 'An unknown error has occured'}
	}
}