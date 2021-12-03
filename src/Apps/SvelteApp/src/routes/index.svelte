<script context="module" lang="ts">
	export const prerender = true;
</script>

<script lang="ts">
	import AuthGuard from '$lib/AuthGuard.svelte';
	import { onMount } from 'svelte';

	let questions;

	onMount(getCalendar);

	function getCalendar(){
		const response =
	}

</script>

<svelte:head>
	<title>Christmas Quiz</title>
</svelte:head>

<AuthGuard>
	<svelte:fragment slot='authed'>
	</svelte:fragment>
	<svelte:fragment slot='not_authed'>
	</svelte:fragment>
</AuthGuard>
