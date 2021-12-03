<script>
	import { goto } from '$app/navigation';
	import { onMount } from 'svelte';
	export let url;

	onMount(() => {
		if(url) goto(url);
	})
</script>