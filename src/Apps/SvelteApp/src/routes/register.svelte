<script>

	import {post, browserSet } from '$lib/req_utils.js';
	import { isAuthenticated } from '../stores/authentication.js';
	import { goto } from '$app/navigation';
	import { GoogleAuth } from '@beyonk/svelte-social-auth';

	let email = '', password = '', name='';

	async function handleLogin() {
		const json = await post(fetch, 'https://localhost:5021/api/Login/Register', { email, password, name })
		if(json.data.token){
			browserSet('jwt',json.data.token);
			isAuthenticated.update(n => true);
			goto('/');
		}

	}

	async function googleAuthSuccess(e){
		const provider = "Google";
		const idToken = e?.detail?.user?.wc?.id_token;
		const json = await post(fetch, 'https://localhost:5021/api/login/external', { provider, idToken })
		if(json?.data?.token){
			browserSet('jwt',json.data.token);
			isAuthenticated.update(n => true);
			goto('/');
		}
	}

	function googleAuthError(e){
		console.dir(e);
	}

	function googleInitError(e){
		console.dir(e);
	}

</script>

<svelte:head>
	<title>Register - Christmas Quiz</title>
</svelte:head>

<div class='bg-green-500 rounded-2xl overflow-hidden p-4 text-white w-3/4 lg:w-1/2 mx-auto shadow-xl'>
	<h1 class='font-bold text-4xl mb-4 text-green-300'>Register</h1>
	<form on:submit|preventDefault={handleLogin} class='flex flex-col'>
		<input type='text' name='name' bind:value={name} placeholder='Your name' class='rounded-lg p-2 mb-2' required/>
		<input type='email' name='email' bind:value={email} placeholder='Your email' class='rounded-lg p-2 mb-2' required/>
		<input type='password' name='password' bind:value={password} placeholder='A great password' class='rounded-lg p-2 mb-4' required />
		<div class='text-right'>
			<button type='submit' class='text-green-500 bg-white px-4 py-2 text-lg font-semibold rounded-lg'>Sign Up</button>
		</div>
	</form>


	<div class='border-t border-white my-4'></div>

	<GoogleAuth clientId="427721046495-vljbnrfkjmnln8t1vn9uq1dhc87oo323.apps.googleusercontent.com" on:auth-success={googleAuthSuccess} on:init-error={googleInitError} on:auth-error={googleAuthError} text='Register with Google' />

</div>
