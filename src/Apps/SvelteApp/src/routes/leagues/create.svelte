<script>

	import {goto} from '$app/navigation';
	import {post, browserSet } from '$lib/req_utils.js';

	let name = '';

	async function handleLogin() {
		const json = await post(fetch, 'https://localhost:5021/api/Leagues', { name })

		if(json.succeeded && json?.data){
			goto(`/leagues/${json.data}`);
		}

	}
</script>

<svelte:head>
	<title>Create League - Christmas Quiz</title>
</svelte:head>

<div class='bg-green-500 rounded-2xl overflow-hidden p-4 text-white w-3/4 lg:w-96 mx-auto shadow-xl'>
	<h1 class='font-bold text-4xl mb-4 text-green-300'>Create League</h1>
	<form on:submit|preventDefault={handleLogin} class='flex flex-col'>
		<input type='text' name='name' bind:value={name} placeholder='League name' class='rounded-lg p-2 mb-2' required autocomplete='off' />
		<div class="flex">
			<div class='flex-1 text-left'>
				<a href='/leagues' class='bg-green-400 text-white px-4 py-2 text-lg font-semibold rounded-lg inline-block'>Cancel</a>
			</div>
			<div class='flex-1 text-right'>
				<button type='submit' class='text-green-500 bg-white px-4 py-2 text-lg font-semibold rounded-lg'>Submit</button>
			</div>
		</div>
	</form>
</div>
