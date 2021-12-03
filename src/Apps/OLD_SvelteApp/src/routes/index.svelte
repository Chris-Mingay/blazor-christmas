<script context="module">
	export const prerender = true;
</script>

<script>
	import {onMount} from "svelte";
	import Counter from '$lib/Counter.svelte';
	import {authenticated} from '../stores/auth';

	let message = ''
	onMount(async () => {
		try {
			const response = await fetch('https://localhost:5021/api/UserProfiles', {
				headers: {'Content-Type': 'application/json'},
				credentials: 'include',
			});
			const content = await response.json();
			message = `Hi ${content.name}`;
			authenticated.set(true);
		} catch (e) {
			message = 'You are not logged in';
			authenticated.set(false);
		}
	});

</script>

<svelte:head>
	<title>Home</title>
</svelte:head>

<section>
	<h1>
		<div class="welcome">
			<picture>
				<source srcset="svelte-welcome.webp" type="image/webp" />
				<img src="svelte-welcome.png" alt="Welcome" />
			</picture>
		</div>

		to your new<br />SvelteKit app
	</h1>

	<h2>
		try editing <strong>src/routes/index.svelte</strong>
	</h2>

	<Counter />
</section>

<style>
	section {
		display: flex;
		flex-direction: column;
		justify-content: center;
		align-items: center;
		flex: 1;
	}

	h1 {
		width: 100%;
	}

	.welcome {
		position: relative;
		width: 100%;
		height: 0;
		padding: 0 0 calc(100% * 495 / 2048) 0;
	}

	.welcome img {
		position: absolute;
		width: 100%;
		height: 100%;
		top: 0;
		display: block;
	}
</style>
