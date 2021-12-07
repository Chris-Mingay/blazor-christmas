<script>

	import { onMount} from 'svelte';
	import { post, put } from '$lib/req_utils.js';
	import { goto } from '$app/navigation';

	let name

	onMount(async() => {
		const res = await fetch('https://localhost:5021/api/UserProfiles',{
			headers: {
				'Authorization': `Bearer ${localStorage.getItem('jwt')}`
			}
		});
		console.log(res);
		const json = await res.json();
		name = json.data.name;
	});

	async function handleSubmit(){
		const json = await put(fetch, 'https://localhost:5021/api/UserProfiles', { name })

		if(json.succeeded){
			goto(`/`);
		}
	}


</script>


<svelte:head>
	<title>Profile - Christmas Quiz</title>
</svelte:head>

<div class='bg-green-500 rounded-2xl overflow-hidden p-4 text-white w-3/4 lg:w-96 mx-auto shadow-xl'>
	<h1 class='font-bold text-4xl mb-4 text-green-300'>Change your name</h1>
	<form on:submit|preventDefault={handleSubmit} class='flex flex-col'>
		<input type='text' name='name' bind:value={name} placeholder='Your nickname' class='rounded-lg p-2 mb-2' required autocomplete='off' />
		<div class="flex">
			<div class='flex-1 text-left'>
				<a href='/' class='bg-green-400 text-white px-4 py-2 text-lg font-semibold rounded-lg inline-block'>Cancel</a>
			</div>
			<div class='flex-1 text-right'>
				<button type='submit' class='text-green-500 bg-white px-4 py-2 text-lg font-semibold rounded-lg'>Submit</button>
			</div>
		</div>
	</form>
</div>
