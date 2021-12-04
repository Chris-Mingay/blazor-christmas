<script lang='ts'>
	import '../app.css';
	import { onMount } from 'svelte';
	import { browserGet, browserSet } from '$lib/req_utils';
	import { goto } from '$app/navigation';
	import { isAuthenticated } from '../stores/authentication';

	let authenticated;

	onMount(() => {
		const token = browserGet('jwt');
		isAuthenticated.update(n => token != null);
	});

	function logout(){
		browserSet('jwt', null);
		isAuthenticated.update(n => false);
		goto('/login');
	}

	isAuthenticated.subscribe(value => {
		authenticated = value;
	});

</script>

<div class='flex flex-col min-h-screen'>
	<div class='flex-0 p-1 border-b border-gray-400'>
		<nav class='flex'>
			<div class='flex-1'>
				<a href='/'>Home</a>
				{#if authenticated}
					<a href='/profile'>Profile</a>
					<a href='/leagues'>Leagues</a>
					<button on:click={logout} class='py-0'>Logout</button>
				{:else}
					<a href='/login'>Login</a>
					<a href='/register'>Register</a>

				{/if}
			</div>
			<div class='flex-0'>
				<a href='/about' class='pull-right'>About</a>
			</div>
		</nav>
	</div>
	<div class='flex-1 flex flex-col items-center justify-center bg-blue-100'>
		<slot />
	</div>
</div>

<style lang='postcss'>
    a,button {
        font-family: 'Lora', serif;
        @apply underline font-semibold text-blue-900 m-2;
    }

		button {
				@apply my-0;
		}
</style>