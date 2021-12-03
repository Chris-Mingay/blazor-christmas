<script>

	import {goto} from '$app/navigation';
	import {post, browserSet } from '$lib/req_utils.js';
	import { isAuthenticated } from '../stores/authentication';

	let email = '', password = '';

	async function handleLogin() {
		const json = await post(fetch, 'https://localhost:5021/api/Login', { email, password })
		if(json?.data?.token){
			browserSet('jwt',json.data.token);
			isAuthenticated.update(n => true);
			goto('/');
		}

	}
</script>

<svelte:head>
	<title>Login - Christmas Quiz</title>
</svelte:head>

<div class='bg-blue-500 rounded-2xl overflow-hidden p-4 text-white w-3/4 lg:w-1/2 mx-auto shadow-xl'>
	<h1 class='font-bold text-4xl mb-4 text-blue-300'>Login</h1>
	<form on:submit|preventDefault={handleLogin} class='flex flex-col'>
		<input type='email' name='email' bind:value={email} placeholder='Email' class='rounded-lg p-2 mb-2'/>
		<input type='password' name='password' bind:value={password} placeholder='Password' class='rounded-lg p-2 mb-4' />
		<div class='text-right'>
			<button type='submit' class='text-blue-500 bg-white px-4 py-2 text-lg font-semibold rounded-lg'>Sign In</button>
		</div>
	</form>
</div>
