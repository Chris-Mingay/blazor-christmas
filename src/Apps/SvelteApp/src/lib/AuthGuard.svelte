<script>
	import { isAuthenticated } from '../stores/authentication.js';
	import { onMount } from 'svelte';

	let authenticated, ready;

	isAuthenticated.subscribe(value => authenticated = value);

	onMount(()=>{
		ready = true;
	})

</script>

{#if ready == true}
	{#if authenticated}
		<slot name="authed"></slot>
	{:else}
		<slot name="not_authed"></slot>
	{/if}
{/if}